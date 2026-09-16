using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IOrganizationsWriteOnlyRepository
{
    Task AddAsync(Organization organization, CancellationToken cancellationToken);
}
