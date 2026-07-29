using Bogus;
using ProdutivAgro.Application.Identity.Commands.ChangePassword;

namespace ProdutivAgro.Testing.Common.Identity.Commands.ChangePassword;

public class ChangePasswordCommandBuilder
{
    private readonly Faker _faker = new();
    private string? _currentPassword;
    private string? _newPassword;

    public ChangePasswordCommandBuilder()
    {
        _currentPassword = _faker.Internet.Password(prefix: "!Aa1");
        _newPassword = _faker.Internet.Password(prefix: "!Aa1");
    }

    public ChangePasswordCommandBuilder WithCurrentPassword(string? password)
    {
        _currentPassword = password;
        return this;
    }

    public ChangePasswordCommandBuilder WithNewPassword(string? password)
    {
        _newPassword = password;
        return this;
    }

    public ChangePasswordCommand Build()
    {
        return new ChangePasswordCommand
        {
            CurrentPassword = _currentPassword!,
            NewPassword = _newPassword!,
        };
    }
}