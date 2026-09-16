namespace ProdutivAgro.Application.Identity.Commands.CreateInvitation;

public sealed class CreateInvitationResult
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; init; }
    public string InvitationUrl { get; init; } = string.Empty;
}