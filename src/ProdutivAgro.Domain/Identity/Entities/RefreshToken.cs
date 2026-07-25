using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Identity.Entities;

public sealed class RefreshToken : Entity
{
    public RefreshToken(Guid userId, string tokenHash, DateTimeOffset expiresAt)
    {
        UserId = userId;
        TokenHash = tokenHash;
        ExpiresAt = expiresAt;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid UserId { get; private set; }
    public string TokenHash { get; private set; }
    public DateTimeOffset ExpiresAt { get; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? RevokedAt { get; private set; }
    public Guid? ReplacedByTokenId { get; private set; }

    public User User { get; private set; } = null!;

    public bool IsUsable(DateTimeOffset now)
    {
        return RevokedAt is null && ExpiresAt > now;
    }

    public void Revoke(Guid replacementTokenId)
    {
        RevokedAt = DateTimeOffset.UtcNow;
        ReplacedByTokenId = replacementTokenId;
    }
}