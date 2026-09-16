using Bogus;
using ProdutivAgro.Application.Identity.Commands.Register;

namespace ProdutivAgro.Testing.Common.Identity.Commands.Register;

public sealed class RegisterCommandBuilder
{
    private readonly Faker _faker = new();
    private string? _email;
    private string? _name;
    private string? _organizationName;
    private string? _password;

    public RegisterCommandBuilder()
    {
        _name = _faker.Person.FullName;
        _email = _faker.Internet.Email();
        _password = "!Aa1validPassword";
        _organizationName = _faker.Company.CompanyName();
    }

    public RegisterCommandBuilder WithName(string? name)
    {
        _name = name;
        return this;
    }

    public RegisterCommandBuilder WithEmail(string? email)
    {
        _email = email;
        return this;
    }

    public RegisterCommandBuilder WithPassword(string? password)
    {
        _password = password;
        return this;
    }

    public RegisterCommandBuilder WithOrganizationName(string? organizationName)
    {
        _organizationName = organizationName;
        return this;
    }

    public RegisterCommand Build()
    {
        return new RegisterCommand
        {
            Name = _name!,
            Email = _email!,
            Password = _password!,
            OrganizationName = _organizationName!,
        };
    }
}