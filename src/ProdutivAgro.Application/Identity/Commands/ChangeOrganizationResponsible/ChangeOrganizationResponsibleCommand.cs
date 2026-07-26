using MediatR;

namespace ProdutivAgro.Application.Identity.Commands.ChangeOrganizationResponsible;

public sealed class ChangeOrganizationResponsibleCommand : IRequest<Unit>
{
    public Guid NewResponsibleUserId { get; init; }
}
