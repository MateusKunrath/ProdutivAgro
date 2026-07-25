using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Identity.Repositories;

namespace ProdutivAgro.Application.Identity.Commands.Logout;

public sealed class LogoutCommandHandler(
    IRefreshTokensReadOnlyRepository refreshTokensReadOnlyRepository,
    IRefreshTokensUpdateOnlyRepository refreshTokensUpdateOnlyRepository,
    IRefreshTokenService refreshTokenService,
    IUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand, Unit>
{
    public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var tokenHash = refreshTokenService.Hash(request.RefreshToken);

        var refreshToken = await refreshTokensReadOnlyRepository.GetByTokenHashAsync(tokenHash, cancellationToken);
        if (refreshToken is not null && refreshToken.RevokedAt is null)
        {
            refreshToken.Revoke();
            refreshTokensUpdateOnlyRepository.Update(refreshToken);
            await unitOfWork.Commit();
        }

        return Unit.Value;
    }
}