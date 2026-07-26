using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IOrganizationsReadOnlyRepository
{
    Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
