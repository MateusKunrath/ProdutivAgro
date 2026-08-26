using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Domain.Identity.Repositories;

public interface IInvitationsWriteOnlyRepository
{
    Task AddAsync(OrganizationInvitation organizationInvitation, CancellationToken cancellationToken);
}