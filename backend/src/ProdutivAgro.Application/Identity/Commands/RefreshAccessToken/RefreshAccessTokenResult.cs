namespace ProdutivAgro.Application.Identity.Commands.RefreshAccessToken;

public sealed class RefreshAccessTokenResult
{
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
}
