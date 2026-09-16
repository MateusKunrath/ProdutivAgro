using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}