using Bogus;
using ProdutivAgro.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestOrganizationJsonBuilder
{
    public static RequestOrganizationJson Build()
    {
        return new Faker<RequestOrganizationJson>()
            .RuleFor(r => r.Name, faker => faker.Company.CompanyName());
    }
}