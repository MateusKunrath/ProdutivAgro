using FluentAssertions;
using ProdutivAgro.Application.Identity.Commands.Login;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;
using ProdutivAgro.Testing.Common.Cryptography;
using ProdutivAgro.Testing.Common.Entities.Users;
using ProdutivAgro.Testing.Common.Identity.Commands.Login;
using ProdutivAgro.Testing.Common.Repositories;
using ProdutivAgro.Testing.Common.Repositories.RefreshTokens;
using ProdutivAgro.Testing.Common.Repositories.Users;
using ProdutivAgro.Testing.Common.Token;

namespace ProdutivAgro.Application.UnitTests.Identity.Commands.Login;

public class LoginCommandHandlerTests
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var command = new LoginCommandBuilder().WithEmail(user.Email).Build();
        var handler = CreateHandler(user, command.Password);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrWhiteSpace();
        result.RefreshToken.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ErrorUserNotFound()
    {
        var user = UserBuilder.Build();
        var command = new LoginCommandBuilder().Build();
        var handler = CreateHandler(user, command.Password);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<InvalidAuthenticationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.IDENTIFIER_OR_PASSWORD_INVALID));
    }

    [Fact]
    public async Task ErrorPasswordDontMatch()
    {
        var user = UserBuilder.Build();
        var command = new LoginCommandBuilder().WithEmail(user.Email).Build();
        var handler = CreateHandler(user);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<InvalidAuthenticationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.IDENTIFIER_OR_PASSWORD_INVALID));
    }

    private LoginCommandHandler CreateHandler(User user, string? password = null)
    {
        return new LoginCommandHandler(
            new UsersReadOnlyRepositoryBuilder().GetByEmailAsync(user).Build(),
            RefreshTokensWriteOnlyRepositoryBuilder.Build(),
            new PasswordEncrypterBuilder().Verify(password).Build(),
            JwtTokenGeneratorBuilder.Build(),
            RefreshTokenServiceBuilder.Build(),
            UnitOfWorkBuilder.Build());
    }
}