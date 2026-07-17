namespace ProdutivAgro.Domain.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}