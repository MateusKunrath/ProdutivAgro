using Bogus;
using ProdutivAgro.Communication.Requests.Users;

namespace CommonTestUtilities.Requests;

public class RequestCreateUserJsonBuilder
{
    public static RequestCreateUserJson Build()
    {
        return new Faker<RequestCreateUserJson>()
               .RuleFor(user => user.Name, faker => faker.Name.FullName())
               .RuleFor(user => user.Email, faker => faker.Internet.Email())
               .RuleFor(user => user.PhoneNumber, faker => faker.Phone.PhoneNumber("###########"))
               .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "!Aa1"))
               .RuleFor(user => user.OrganizationId, faker => faker.Random.Guid());
    }
}