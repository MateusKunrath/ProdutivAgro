using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Identity.Commands.Login;

public sealed class LoginCommandHandler(
    IUsersReadOnlyRepository usersReadOnlyRepository,
    IRefreshTokensWriteOnlyRepository refreshTokensWriteOnlyRepository,
    IPasswordEncrypter passwordEncrypter,
    IJwtTokenGenerator jwtTokenGenerator,
    IRefreshTokenService refreshTokenService,
    IUnitOfWork unitOfWork) : IRequestHandler<LoginCommand, LoginResult>
{
    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var user = await usersReadOnlyRepository.GetByEmailAsync(request.Email.Trim(), cancellationToken);

        if (user is null ||
            user.Active != UserStatus.Active ||
            !passwordEncrypter.Verify(request.Password, user.Password))
        {
            throw new InvalidAuthenticationException();
        }

        var rawRefreshToken = refreshTokenService.Generate();
        var refreshToken = new RefreshToken(
            user.Id,
            refreshTokenService.Hash(rawRefreshToken),
            refreshTokenService.GetExpirationDate(DateTimeOffset.UtcNow));

        await refreshTokensWriteOnlyRepository.AddAsync(refreshToken, cancellationToken);
        await unitOfWork.Commit();

        return new LoginResult
        {
            AccessToken = jwtTokenGenerator.Generate(user),
            RefreshToken = rawRefreshToken,
        };
    }

    private async Task Validate(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await new LoginCommandValidator().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}