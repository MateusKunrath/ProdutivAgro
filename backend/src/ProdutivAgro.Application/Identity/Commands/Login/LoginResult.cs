namespace ProdutivAgro.Application.Identity.Commands.Login;

public sealed class LoginResult
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
}
