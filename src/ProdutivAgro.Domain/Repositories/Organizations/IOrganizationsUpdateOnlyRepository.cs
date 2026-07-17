using ProdutivAgro.Domain.Entities;

namespace ProdutivAgro.Domain.Repositories.Organizations;

public interface IOrganizationsUpdateOnlyRepository
{
    Task<Organization?> GetById(Guid id);
    void Update(Organization organization);
}