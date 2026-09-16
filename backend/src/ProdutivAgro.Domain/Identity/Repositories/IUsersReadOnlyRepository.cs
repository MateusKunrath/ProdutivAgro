using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IUsersReadOnlyRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> ExistsUserWithSameEmailAsync(string email, CancellationToken cancellationToken);
}