using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IUsersUpdateReadOnlyRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Update(User user);
}