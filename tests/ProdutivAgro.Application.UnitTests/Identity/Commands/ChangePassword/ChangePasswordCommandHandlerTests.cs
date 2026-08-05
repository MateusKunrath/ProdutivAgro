using FluentAssertions;
using ProdutivAgro.Application.Identity.Commands.ChangePassword;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;
using ProdutivAgro.Testing.Common.Cryptography;
using ProdutivAgro.Testing.Common.CurrentUser;
using ProdutivAgro.Testing.Common.Entities.Users;
using ProdutivAgro.Testing.Common.Identity.Commands.ChangePassword;
using ProdutivAgro.Testing.Common.Repositories;
using ProdutivAgro.Testing.Common.Repositories.RefreshTokens;
using ProdutivAgro.Testing.Common.Repositories.Users;

namespace ProdutivAgro.Application.UnitTests.Identity.Commands.ChangePassword;

public class ChangePasswordCommandHandlerTests
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var command = new ChangePasswordCommandBuilder().WithCurrentPassword(user.Password).Build();
        var handler = CreateHandler(user, user.Password);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ErrorCurrentPasswordNotMatch()
    {
        var user = UserBuilder.Build();
        var command = new ChangePasswordCommandBuilder().Build();
        var handler = CreateHandler(user, user.Password);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 &&
            ex.GetErrors().Contains(ResourceErrorMessages.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
    }

    [Fact]
    public async Task ErrorCurrentPasswordEmpty()
    {
        var user = UserBuilder.Build();
        var command = new ChangePasswordCommandBuilder().WithCurrentPassword(string.Empty).Build();
        var handler = CreateHandler(user, user.Password);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 &&
            ex.GetErrors().Contains(ResourceErrorMessages.CURRENT_PASSWORD_EMPTY));
    }

    [Fact]
    public async Task ErrorUserNotFound()
    {
        var command = new ChangePasswordCommandBuilder().Build();
        var handler = CreateHandler();

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 &&
            ex.GetErrors().Contains(ResourceErrorMessages.USER_NOT_FOUND));
    }

    [Fact]
    public async Task ErrorNewPasswordEmpty()
    {
        var user = UserBuilder.Build();
        var command = new ChangePasswordCommandBuilder()
                      .WithCurrentPassword(user.Password)
                      .WithNewPassword(string.Empty)
                      .Build();
        var handler = CreateHandler(user, user.Password);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.PASSWORD_INVALID));
    }

    [Fact]
    public async Task ErrorNewPasswordInvalid()
    {
        var user = UserBuilder.Build();
        var command = new ChangePasswordCommandBuilder()
                      .WithCurrentPassword(user.Password)
                      .WithNewPassword("abc1323asdd")
                      .Build();
        var handler = CreateHandler(user, user.Password);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.PASSWORD_INVALID));
    }

    private ChangePasswordCommandHandler CreateHandler(User? user = null, string? password = null)
    {
        var usersUpdateOnlyRepositoryBuilder = new UsersUpdateOnlyRepositoryBuilder();
        var currentUserBuilder = new CurrentUserBuilder();
        if (user is not null)
        {
            usersUpdateOnlyRepositoryBuilder.GetById(user);
            currentUserBuilder.DefineUser(user);
        }

        var passwordEncrypterBuilder = new PasswordEncrypterBuilder();
        if (password is not null)
        {
            passwordEncrypterBuilder.Verify(password);
        }

        return new ChangePasswordCommandHandler(
            usersUpdateOnlyRepositoryBuilder.Build(),
            RefreshTokensUpdateOnlyRepositoryBuilder.Build(),
            UnitOfWorkBuilder.Build(),
            currentUserBuilder.Build(),
            passwordEncrypterBuilder.Build());
    }
}