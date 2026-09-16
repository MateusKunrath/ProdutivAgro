using FluentAssertions;
using ProdutivAgro.Application.Identity.Queries.GetCurrentUser;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Extensions;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;
using ProdutivAgro.Testing.Common.CurrentUser;
using ProdutivAgro.Testing.Common.Entities.Users;
using ProdutivAgro.Testing.Common.Repositories.Users;

namespace ProdutivAgro.Application.UnitTests.Identity.Queries.GetCurrentUser;

public sealed class GetCurrentUserQueryHandlerTests
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var handler = CreateHandler(user);

        var result = await handler.Handle(new GetCurrentUserQuery(), CancellationToken.None);

        result.Id.Should().Be(user.Id);
        result.Name.Should().Be(user.Name);
        result.Email.Should().Be(user.Email);
        result.Organization.Id.Should().Be(user.OrganizationId);
        result.Role.Should().Be(user.Role.RoleToString());
        result.Status.Should().Be(user.Active.StatusToString());
    }

    [Fact]
    public async Task ErrorUserNotFound()
    {
        var handler = CreateHandler();

        var act = async () => await handler.Handle(new GetCurrentUserQuery(), CancellationToken.None);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(ex => ex.GetErrors().Single() == ResourceErrorMessages.USER_NOT_FOUND);
    }

    private static GetCurrentUserQueryHandler CreateHandler(User? user = null)
    {
        var usersReadOnlyRepositoryBuilder = new UsersReadOnlyRepositoryBuilder();
        var currentUserBuilder = new CurrentUserBuilder();

        if (user is not null)
        {
            usersReadOnlyRepositoryBuilder.GetByIdAsync(user);
            currentUserBuilder.DefineUser(user);
        }

        return new GetCurrentUserQueryHandler(
            usersReadOnlyRepositoryBuilder.Build(),
            currentUserBuilder.Build());
    }
}
