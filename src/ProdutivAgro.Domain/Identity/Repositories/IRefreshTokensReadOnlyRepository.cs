using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IRefreshTokensReadOnlyRepository
{
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken);
}
