using Bogus;
using ProdutivAgro.Application.Identity.Commands.Login;

namespace ProdutivAgro.Testing.Common.Identity.Commands.Login;

public sealed class LoginCommandBuilder
{
    private readonly Faker _faker = new();
    private string? _email;
    private string? _password;

    public LoginCommandBuilder()
    {
        _email = _faker.Internet.Email();
        _password = _faker.Internet.Password(prefix: "!Aa1");
    }

    public LoginCommandBuilder WithEmail(string? email)
    {
        _email = email;
        return this;
    }

    public LoginCommandBuilder WithPassword(string? password)
    {
        _password = password;
        return this;
    }

    public LoginCommand Build()
    {
        return new LoginCommand
        {
            Email = _email!,
            Password = _password!,
        };
    }
}