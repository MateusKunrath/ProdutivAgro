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
        var user = new User(
            request.Name,
            request.Email,
            organization.Id,
            UserRole.Administrator);
        user.SetPasswordHash(passwordEncrypter.Encrypt(request.Password));

        var rawRefreshToken = refreshTokenService.Generate();
        var refreshToken = new RefreshToken(
            user.Id,
            refreshTokenService.Hash(rawRefreshToken),
            refreshTokenService.GetExpirationDate(DateTimeOffset.UtcNow));

        await unitOfWork.ExecuteInTransactionAsync(async cancelToken =>
        {
            await organizationsWriteOnlyRepository.AddAsync(organization, cancelToken);
            await usersWriteOnlyRepository.AddAsync(user);
            await refreshTokensWriteOnlyRepository.AddAsync(refreshToken, cancelToken);
            organization.SetResponsibleUser(user.Id);
        }, cancellationToken);

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
