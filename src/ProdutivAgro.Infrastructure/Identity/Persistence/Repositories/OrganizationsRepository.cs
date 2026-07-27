using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Infrastructure.Persistence;

namespace ProdutivAgro.Infrastructure.Identity.Persistence.Repositories;

public class OrganizationsRepository(ProdutivAgroDbContext dbContext)
    : IOrganizationsWriteOnlyRepository, IOrganizationsReadOnlyRepository, IOrganizationsUpdateReadOnlyRepository
{
    async Task<Organization?> IOrganizationsReadOnlyRepository.GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return await dbContext.Organizations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAndIsActiveAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Organizations
                              .AsNoTracking()
                              .Where(x => x.Active)
                              .AnyAsync(x => x.Id == id, cancellationToken);
    }

    async Task<Organization?> IOrganizationsUpdateReadOnlyRepository.GetByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return await dbContext.Organizations.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Organization organization, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(organization, cancellationToken);
    }
}