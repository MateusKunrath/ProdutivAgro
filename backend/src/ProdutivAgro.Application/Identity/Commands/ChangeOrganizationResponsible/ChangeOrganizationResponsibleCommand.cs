using MediatR;

using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Identity.Commands.ChangeOrganizationResponsible;

public sealed class ChangeOrganizationResponsibleCommand : IRequest<Unit>, IRequireActiveOrganization
{
    public Guid NewResponsibleUserId { get; init; }
}
