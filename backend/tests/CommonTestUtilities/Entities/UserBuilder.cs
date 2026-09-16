using Bogus;
using CommonTestUtilities.Cryptography;
using ProdutivAgro.Domain.Entities;

namespace CommonTestUtilities.Entities;

public class UserBuilder
{
    public static User Build(Guid organizationId = default)
    {
        var passwordEncrypter = new PasswordEncrypterBuilder().Build();

        return new Faker<User>()
               .RuleFor(user => user.Id, _ => Guid.NewGuid())
               .RuleFor(user => user.Name, faker => faker.Name.FullName())
               .RuleFor(user => user.Email, faker => faker.Internet.Email())
               .RuleFor(user => user.PhoneNumber, faker => faker.Phone.PhoneNumber())
               .RuleFor(user => user.Password, (_, user) => passwordEncrypter.Encrypt(user.Password))
               .RuleFor(user => user.OrganizationId, _ => organizationId);
    }
}