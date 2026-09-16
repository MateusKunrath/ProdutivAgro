using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IRefreshTokensWriteOnlyRepository
{
    Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
}
