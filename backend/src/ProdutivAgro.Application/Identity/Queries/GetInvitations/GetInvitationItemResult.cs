using ProdutivAgro.Application.Shared;

namespace ProdutivAgro.Application.Identity.Queries.GetInvitations;

public class GetInvitationItemResult
{
    public Guid Id { get; init; }
    public OrganizationIdResult Organization { get; init; } = new();
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public UserResult InvitedByUser { get; init; } = new();
    public DateTimeOffset? AcceptedAt { get; init; }
    public DateTimeOffset? RevokedAt { get; init; }
    public DateTimeOffset ExpiresAt { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}