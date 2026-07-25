using AutoMapper;
using FluentValidation.Results;
using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Identity.Commands.Register;

public class RegisterCommandHandler(
    IUsersReadOnlyRepository usersReadOnlyRepository,
    IUsersWriteOnlyRepository usersWriteOnlyRepository,
    IOrganizationsWriteOnlyRepository organizationsWriteOnlyRepository,
    IMapper mapper,
    IPasswordEncrypter passwordEncrypter,
    IJwtTokenGenerator jwtTokenGenerator,
    IRefreshTokenService refreshTokenService,
    IRefreshTokensWriteOnlyRepository refreshTokensWriteOnlyRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RegisterCommand, RegisterResult>
{
    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var organization = new Organization(request.OrganizationName);
        await organizationsWriteOnlyRepository.AddAsync(organization, cancellationToken);

        var user = mapper.Map<User>(request);
        user.SetOrganization(organization);
        user.SetPasswordHash(passwordEncrypter.Encrypt(request.Password));
        user.SetRole(UserRole.Administrator);
        await usersWriteOnlyRepository.AddAsync(user);

        var rawRefreshToken = refreshTokenService.Generate();
        var refreshToken = new RefreshToken(
            user.Id,
            refreshTokenService.Hash(rawRefreshToken),
            refreshTokenService.GetExpirationDate(DateTimeOffset.UtcNow));

        await refreshTokensWriteOnlyRepository.AddAsync(refreshToken, cancellationToken);
        await unitOfWork.Commit();

        return new RegisterResult
        {
            Name = user.Name,
            AccessToken = jwtTokenGenerator.Generate(user),
            RefreshToken = rawRefreshToken,
        };
    }

    private async Task Validate(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await new RegisterCommandValidator().ValidateAsync(request, cancellationToken);

        var emailExists = await usersReadOnlyRepository.ExistsUserWithSameEmailAsync(request.Email, cancellationToken);
        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
