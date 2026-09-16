using FluentAssertions;
using ProdutivAgro.Application.Identity.Commands.Register;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;
using ProdutivAgro.Testing.Common.Cryptography;
using ProdutivAgro.Testing.Common.Identity.Commands.Register;
using ProdutivAgro.Testing.Common.Repositories;
using ProdutivAgro.Testing.Common.Repositories.Organizations;
using ProdutivAgro.Testing.Common.Repositories.RefreshTokens;
using ProdutivAgro.Testing.Common.Repositories.Users;
using ProdutivAgro.Testing.Common.Token;

namespace ProdutivAgro.Application.UnitTests.Identity.Commands.Register;

public class RegisterCommandHandlerTests
{
    [Fact]
    public async Task Success()
    {
        var request = new RegisterCommandBuilder().Build();
        var handler = CreateHandler();

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.AccessToken.Should().NotBeNullOrWhiteSpace();
        result.RefreshToken.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ErrorNameEmpty()
    {
        var command = new RegisterCommandBuilder().WithName(string.Empty).Build();
        var handler = CreateHandler();

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
    }

    [Fact]
    public async Task ErrorEmailAlreadyExists()
    {
        var command = new RegisterCommandBuilder().Build();
        var handler = CreateHandler(command.Email);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
    }

    [Fact]
    public async Task ErrorOrganizationNameEmpty()
    {
        var command = new RegisterCommandBuilder().WithOrganizationName(string.Empty).Build();
        var handler = CreateHandler();

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.ORGANIZATION_NAME_EMPTY));
    }


    private RegisterCommandHandler CreateHandler(string? email = null)
    {
        var usersReadOnlyRepository = new UsersReadOnlyRepositoryBuilder();
        if (!string.IsNullOrWhiteSpace(email))
        {
            usersReadOnlyRepository.ExistsUserWithSameEmailAsync(email);
        }

        return new RegisterCommandHandler(
            usersReadOnlyRepository.Build(),
            UsersWriteOnlyRepositoryBuilder.Build(),
            OrganizationsWriteOnlyRepositoryBuilder.Build(),
            new PasswordEncrypterBuilder().Build(),
            JwtTokenGeneratorBuilder.Build(),
            RefreshTokenServiceBuilder.Build(),
            RefreshTokensWriteOnlyRepositoryBuilder.Build(),
            UnitOfWorkBuilder.Build());
    }
}