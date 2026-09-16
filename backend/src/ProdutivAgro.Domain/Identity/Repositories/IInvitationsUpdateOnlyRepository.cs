using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IInvitationsUpdateOnlyRepository
{
    Task<OrganizationInvitation?> GetPendingByOrganizationIdAndEmailAsync(Guid organizationId, string email,
        CancellationToken cancellationToken);
}