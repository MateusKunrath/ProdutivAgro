using ProdutivAgro.Domain.Entities;

namespace ProdutivAgro.Domain.Repositories.Users;

public interface IUsersWriteOnlyRepository
{
    Task Add(User user);
}