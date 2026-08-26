using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Identity.Entities;

public class OrganizationInvitation : Entity
{
    public Guid OrganizationId { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }
    public string TokenHash { get; private set; } = string.Empty;
    public Guid InvitedByUserId { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? AcceptedAt { get; private set; }
    public DateTimeOffset? RevokedAt { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
}
