using FluentAssertions;
using ProdutivAgro.Application.Shared;

namespace ProdutivAgro.Application.UnitTests.Shared;

public class OrganizationIdResultTests
{
    [Fact]
    public void ContainsOnlyTheOrganizationIdentifier()
    {
        var organizationId = Guid.NewGuid();
        var result = new OrganizationIdResult { Id = organizationId };

        result.Id.Should().Be(organizationId);
        typeof(OrganizationIdResult).GetProperties().Should().ContainSingle();
    }
}
