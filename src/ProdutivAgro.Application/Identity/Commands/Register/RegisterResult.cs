namespace ProdutivAgro.Application.Identity.Commands.Register;

public class RegisterResult
{
    public string Name { get; init; } = string.Empty;
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
}
