namespace ProdutivAgro.Api.Contracts.Identity;

public sealed class RefreshAccessTokenRequest
{
    public string RefreshToken { get; init; } = string.Empty;
}
