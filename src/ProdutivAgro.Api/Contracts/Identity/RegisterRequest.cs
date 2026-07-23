namespace ProdutivAgro.Api.Contracts.Identity;

public sealed class RegisterRequest
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public Guid OrganizationId { get; init; }
}