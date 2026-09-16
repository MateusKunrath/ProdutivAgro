using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Identity.Entities;

public class OrganizationInvitation : Entity
{
    public OrganizationInvitation(
        Guid organizationId,
        string email,
        UserRole role,
        string tokenHash,
        Guid invitedByUserId,
        DateTimeOffset expiresAt)
    {
        OrganizationId = organizationId;
        Email = email;
        Role = role;
        TokenHash = tokenHash;
        InvitedByUserId = invitedByUserId;
        ExpiresAt = expiresAt;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid OrganizationId { get; private set; }
    public string Email { get; private set; }
    public UserRole Role { get; private set; }
    public string TokenHash { get; private set; }
    public Guid InvitedByUserId { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? AcceptedAt { get; private set; }
    public DateTimeOffset? RevokedAt { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public User InvitedByUser { get; private set; } = null!;

    public void Revoke(DateTimeOffset revokedAt)
    {
        RevokedAt = revokedAt;
    }
}