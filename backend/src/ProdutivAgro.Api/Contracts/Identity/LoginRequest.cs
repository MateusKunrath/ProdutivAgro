namespace ProdutivAgro.Api.Contracts.Identity;

public sealed class LoginRequest
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
