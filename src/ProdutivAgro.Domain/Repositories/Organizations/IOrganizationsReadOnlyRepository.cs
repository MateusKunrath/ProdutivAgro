namespace ProdutivAgro.Domain.Repositories.Organizations;

public interface IOrganizationsReadOnlyRepository
{
    Task<bool> ExistActiveOrganizationWithName(string name);
}