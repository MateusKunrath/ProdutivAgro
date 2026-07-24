using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Infrastructure.Persistence;

namespace ProdutivAgro.Infrastructure.Identity.Persistence.Repositories;

public class OrganizationsRepository(ProdutivAgroDbContext dbContext) : IOrganizationsWriteOnlyRepository
{
    public async Task AddAsync(Organization organization, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(organization, cancellationToken);
    }
}