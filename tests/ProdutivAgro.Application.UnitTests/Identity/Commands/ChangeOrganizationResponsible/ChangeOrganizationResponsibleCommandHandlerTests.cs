using FluentAssertions;
using ProdutivAgro.Application.Identity.Commands.ChangeOrganizationResponsible;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;
using ProdutivAgro.Testing.Common.CurrentUser;
using ProdutivAgro.Testing.Common.Entities.Organizations;
using ProdutivAgro.Testing.Common.Entities.Users;
using ProdutivAgro.Testing.Common.Identity.Commands.ChangeOrganizationResponsible;
using ProdutivAgro.Testing.Common.Repositories;
using ProdutivAgro.Testing.Common.Repositories.Organizations;
using ProdutivAgro.Testing.Common.Repositories.Users;

namespace ProdutivAgro.Application.UnitTests.Identity.Commands.ChangeOrganizationResponsible;

public class ChangeOrganizationResponsibleCommandHandlerTests
{
    [Fact]
    public async Task Success()
    {
        var organization = new OrganizationBuilder().Build();
        var responsible = UserBuilder.Build(UserRole.Administrator, organization.Id);
        organization.SetResponsibleUser(responsible.Id);

        var user = UserBuilder.Build(UserRole.Administrator, organization.Id);
        var command = new ChangeOrganizationResponsibleCommandBuilder().WithNewResponsibleUserId(user.Id).Build();
        var handler = CreateHandler(user, responsible, organization);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ErrorNewResponsibleUserIdEmpty()
    {
        var user = UserBuilder.Build();
        var organization = new OrganizationBuilder().WithResponsibleId(user.Id).Build();
        var command = new ChangeOrganizationResponsibleCommandBuilder().WithNewResponsibleUserId(Guid.Empty).Build();
        var handler = CreateHandler(user, user, organization);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.RESPONSIBLE_EMPTY));
    }

    [Fact]
    public async Task ErrorOrganizationNotFound()
    {
        var user = UserBuilder.Build();
        var command = new ChangeOrganizationResponsibleCommandBuilder().Build();
        var handler = CreateHandler(user);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.ORGANIZATION_NOT_FOUND));
    }

    [Fact]
    public async Task ErrorResponsibleNotBePartOfTheOrganizationError()
    {
        var organization = new OrganizationBuilder().Build();
        var responsible = UserBuilder.Build(UserRole.Administrator, organization.Id);
        organization.SetResponsibleUser(responsible.Id);

        var user = UserBuilder.Build();
        var command = new ChangeOrganizationResponsibleCommandBuilder().WithNewResponsibleUserId(user.Id).Build();
        var handler = CreateHandler(user, responsible, organization);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex =>
            ex.GetErrors().Count == 1 &&
            ex.GetErrors().Contains(ResourceErrorMessages.RESPONSIBLE_MUST_BE_PART_OF_THE_ORGANIZATION));
    }

    private ChangeOrganizationResponsibleCommandHandler CreateHandler(User user, User? responsible = null,
        Organization? organization = null)
    {
        var organizationsUpdateOnlyRepositoryBuilder = new OrganizationsUpdateOnlyRepositoryBuilder();
        if (organization is not null)
        {
            organizationsUpdateOnlyRepositoryBuilder.GetById(organization);
        }

        var currentUserBuilder = new CurrentUserBuilder();
        if (responsible is not null)
        {
            currentUserBuilder.DefineUser(responsible);
        }

        return new ChangeOrganizationResponsibleCommandHandler(
            organizationsUpdateOnlyRepositoryBuilder.Build(),
            new UsersReadOnlyRepositoryBuilder().GetByIdAsync(user).Build(),
            currentUserBuilder.Build(),
            UnitOfWorkBuilder.Build());
    }
}