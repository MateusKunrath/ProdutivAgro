namespace ProdutivAgro.Api.Contracts.Organizations;

public sealed class CreateInvitationRequest
{
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}