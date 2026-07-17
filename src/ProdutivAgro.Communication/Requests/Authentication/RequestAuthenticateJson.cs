namespace ProdutivAgro.Communication.Requests.Authentication;

public class RequestAuthenticateJson
{
    public string Identifier { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}