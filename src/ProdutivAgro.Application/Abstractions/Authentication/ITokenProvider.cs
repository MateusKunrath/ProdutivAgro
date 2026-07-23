namespace ProdutivAgro.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string TokenOnRequest();
}