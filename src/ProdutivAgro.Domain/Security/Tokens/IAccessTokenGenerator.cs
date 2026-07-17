using ProdutivAgro.Domain.Entities;

namespace ProdutivAgro.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}