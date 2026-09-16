using ProdutivAgro.Application.Identity.Commands.ChangeOrganizationResponsible;

namespace ProdutivAgro.Testing.Common.Identity.Commands.ChangeOrganizationResponsible;

public sealed class ChangeOrganizationResponsibleCommandBuilder
{
    private Guid _newResponsibleUserId = Guid.NewGuid();

    public ChangeOrganizationResponsibleCommandBuilder WithNewResponsibleUserId(Guid id)
    {
        _newResponsibleUserId = id;
        return this;
    }

    public ChangeOrganizationResponsibleCommand Build() => new()
    {
        NewResponsibleUserId = _newResponsibleUserId,
    };
}
