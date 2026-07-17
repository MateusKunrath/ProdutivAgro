using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Entities;
using ProdutivAgro.Domain.Repositories.Organizations;

namespace ProdutivAgro.Infrastructure.DataAccess.Repositories;

internal class OrganizationsRepository(ProdutivAgroDbContext dbContext)
    : IOrganizationsReadOnlyRepository, IOrganizationsWriteOnlyRepository, IOrganizationsUpdateOnlyRepository
{
    public async Task<bool> ExistActiveOrganizationWithName(string name)
    {
        return await dbContext.Organizations.AsNoTracking().AnyAsync(organization => organization.Name.Equals(name));
    }

    async Task<Organization?> IOrganizationsUpdateOnlyRepository.GetById(Guid id)
    {
        return await dbContext.Organizations.FirstOrDefaultAsync(organization => organization.Id.Equals(id));
    }

    public void Update(Organization organization)
    {
        throw new NotImplementedException();
    }

    public async Task Add(Organization organization)
    {
        await dbContext.Organizations.AddAsync(organization);
    }
}