using Moq;
using ProdutivAgro.Domain.Identity.Repositories;

namespace ProdutivAgro.Testing.Common.Repositories.Organizations;

public class OrganizationsWriteOnlyRepositoryBuilder
{
    public static IOrganizationsWriteOnlyRepository Build()
    {
        var mock = new Mock<IOrganizationsWriteOnlyRepository>();
        return mock.Object;
    }
}