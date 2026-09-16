using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Identity.Queries.GetCurrentOrganization;

public sealed class GetCurrentOrganizationQueryHandler(
    IOrganizationsReadOnlyRepository organizationsReadOnlyRepository,
    ICurrentUser currentUser) : IRequestHandler<GetCurrentOrganizationQuery, GetCurrentOrganizationResult>
{
    public async Task<GetCurrentOrganizationResult> Handle(GetCurrentOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await organizationsReadOnlyRepository.GetByIdAsync(currentUser.OrganizationId, cancellationToken);
        if (organization is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ORGANIZATION_NOT_FOUND);
        }

        return new GetCurrentOrganizationResult
        {
            Id = organization.Id,
            Name = organization.Name,
            Active = organization.Active,
            ResponsibleUserId = organization.ResponsibleUserId,
        };
    }
}
