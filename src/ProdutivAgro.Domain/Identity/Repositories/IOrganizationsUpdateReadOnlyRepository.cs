using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IOrganizationsUpdateReadOnlyRepository
{
    Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
