using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IRefreshTokensUpdateOnlyRepository
{
    void Update(RefreshToken refreshToken);
    Task RevokeAllActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}