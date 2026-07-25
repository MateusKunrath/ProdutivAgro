namespace ProdutivAgro.Api.Contracts.Identity;

public sealed class LogoutRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}