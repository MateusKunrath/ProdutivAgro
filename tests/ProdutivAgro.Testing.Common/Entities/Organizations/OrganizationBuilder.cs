using Bogus;
using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Testing.Common.Entities.Organizations;

public class OrganizationBuilder
{
    private readonly Faker _faker = new();
    private readonly string? _name;
    private Guid? _responsibleUserId;

    public OrganizationBuilder()
    {
        _name = _faker.Company.CompanyName();
        _responsibleUserId = _faker.Random.Guid();
    }

    public OrganizationBuilder WithResponsibleId(Guid? userId)
    {
        _responsibleUserId = userId;
        return this;
    }

    public Organization Build()
    {
        var organization = new Organization(_name!);
        organization.SetResponsibleUser(_responsibleUserId ?? Guid.NewGuid());
        return organization;
    }
}