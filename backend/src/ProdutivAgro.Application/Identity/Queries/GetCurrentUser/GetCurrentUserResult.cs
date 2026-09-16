using ProdutivAgro.Application.Shared;

namespace ProdutivAgro.Application.Identity.Queries.GetCurrentUser;

public sealed class GetCurrentUserResult
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public OrganizationIdResult Organization { get; init; } = new();
    public string Role { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
}