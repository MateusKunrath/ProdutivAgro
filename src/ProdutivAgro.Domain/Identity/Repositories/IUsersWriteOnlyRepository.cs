using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IUsersWriteOnlyRepository
{
    Task AddAsync(User user);
}