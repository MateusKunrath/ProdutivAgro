using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Identity.Commands.RefreshAccessToken;

public sealed class RefreshAccessTokenCommandHandler(
    IRefreshTokensReadOnlyRepository refreshTokensReadOnlyRepository,
    IRefreshTokensWriteOnlyRepository refreshTokensWriteOnlyRepository,
    IRefreshTokensUpdateOnlyRepository refreshTokensUpdateOnlyRepository,
    IUsersReadOnlyRepository usersReadOnlyRepository,
    IRefreshTokenService refreshTokenService,
    IJwtTokenGenerator jwtTokenGenerator,
    IUnitOfWork unitOfWork) : IRequestHandler<RefreshAccessTokenCommand, RefreshAccessTokenResult>
{
    public async Task<RefreshAccessTokenResult> Handle(RefreshAccessTokenCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            throw new InvalidAuthenticationException();
        }

        var tokenHash = refreshTokenService.Hash(request.RefreshToken);
        var currentToken = await refreshTokensReadOnlyRepository.GetByTokenHashAsync(tokenHash, cancellationToken);

        if (currentToken is null || !currentToken.IsUsable(DateTimeOffset.UtcNow))
        {
            throw new InvalidAuthenticationException();
        }

        var user = await usersReadOnlyRepository.GetByIdAsync(currentToken.UserId, cancellationToken);
        if (user is null || user.Active != UserStatus.Active)
        {
            throw new InvalidAuthenticationException();
        }

        var rawReplacementToken = refreshTokenService.Generate();
        var replacementToken = new RefreshToken(
            user.Id,
            refreshTokenService.Hash(rawReplacementToken),
            refreshTokenService.GetExpirationDate(DateTimeOffset.UtcNow));

        currentToken.Revoke(replacementToken.Id);
        refreshTokensUpdateOnlyRepository.Update(currentToken);
        await refreshTokensWriteOnlyRepository.AddAsync(replacementToken, cancellationToken);
        await unitOfWork.Commit();

        return new RefreshAccessTokenResult
        {
            AccessToken = jwtTokenGenerator.Generate(user),
            RefreshToken = rawReplacementToken,
        };
    }
}
