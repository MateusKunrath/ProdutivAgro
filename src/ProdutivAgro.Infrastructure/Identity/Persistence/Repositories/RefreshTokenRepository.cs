using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Infrastructure.Persistence;

namespace ProdutivAgro.Infrastructure.Identity.Persistence.Repositories;

public sealed class RefreshTokenRepository(ProdutivAgroDbContext dbContext) :
    IRefreshTokensReadOnlyRepository,
    IRefreshTokensWriteOnlyRepository,
    IRefreshTokensUpdateOnlyRepository
{
    public Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken)
    {
        return dbContext.RefreshTokens.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);
    }

    public void Update(RefreshToken refreshToken)
    {
        dbContext.RefreshTokens.Update(refreshToken);
    }

    public async Task RevokeAllActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        await dbContext.RefreshTokens
                       .Where(token =>
                           token.UserId == userId &&
                           token.RevokedAt == null &&
                           token.ExpiresAt > DateTimeOffset.UtcNow)
                       .ExecuteUpdateAsync(
                           setters => setters
                               .SetProperty(token => token.RevokedAt, DateTimeOffset.UtcNow),
                           cancellationToken);
    }

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
    }
}