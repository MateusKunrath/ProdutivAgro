using ProdutivAgro.Domain.Entities;

namespace ProdutivAgro.Domain.Services.AuthenticatedUser;

public interface IAuthenticatedUser
{
    Task<User> Get();
}