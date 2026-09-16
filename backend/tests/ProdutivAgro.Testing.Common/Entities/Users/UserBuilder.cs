using Bogus;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Testing.Common.Cryptography;

namespace ProdutivAgro.Testing.Common.Entities.Users;

public class UserBuilder
{
    public static User Build(UserRole userRole = UserRole.Administrator, Guid? organizationId = null)
    {
        var faker = new Faker();
        var passwordEncrypter = new PasswordEncrypterBuilder().Build();

        var user = new User(
            faker.Name.FullName(),
            faker.Internet.Email(),
            organizationId ?? Guid.NewGuid(),
            userRole);

        user.SetPasswordHash(passwordEncrypter.Encrypt(faker.Internet.Password(prefix: "!Aa1")));

        return user;
    }
}