namespace ProdutivAgro.Application.Abstractions.Authentication;

public interface IInvitationUrlGenerator
{
    string Generate(string token);
}