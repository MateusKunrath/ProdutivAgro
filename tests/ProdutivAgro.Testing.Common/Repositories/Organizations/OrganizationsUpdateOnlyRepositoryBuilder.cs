using Moq;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;

namespace ProdutivAgro.Testing.Common.Repositories.Organizations;

public class OrganizationsUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IOrganizationsUpdateReadOnlyRepository> _repository;

    public OrganizationsUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IOrganizationsUpdateReadOnlyRepository>();
    }

    public OrganizationsUpdateOnlyRepositoryBuilder GetById(Organization? organization)
    {
        if (organization is not null)
        {
            _repository.Setup(repository => repository.GetByIdAsync(organization.Id, CancellationToken.None))
                       .ReturnsAsync(organization);
        }

        return this;
    }

    public IOrganizationsUpdateReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}