using ProdutivAgro.Domain.Entities;

namespace ProdutivAgro.Domain.Repositories.Organizations;

public interface IOrganizationsWriteOnlyRepository
{
    Task Add(Organization organization);
}