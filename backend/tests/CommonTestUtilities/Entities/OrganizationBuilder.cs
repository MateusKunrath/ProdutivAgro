using Bogus;
using ProdutivAgro.Domain.Entities;

namespace CommonTestUtilities.Entities;

public class OrganizationBuilder
{
    public static Organization Build()
    {
        return new Faker<Organization>()
               .RuleFor(o => o.Id, _ => Guid.NewGuid())
               .RuleFor(o => o.Name, f => f.Company.CompanyName());
    }
}