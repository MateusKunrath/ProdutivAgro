using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Infrastructure.Persistence;

namespace ProdutivAgro.Infrastructure.Identity.Persistence.Repositories;

public class OrganizationInvitationsRepository(ProdutivAgroDbContext dbContext)
    : IInvitationsUpdateOnlyRepository, IInvitationsWriteOnlyRepository
{
    public async Task<OrganizationInvitation?> GetPendingByOrganizationIdAndEmailAsync(Guid organizationId,
        string email, CancellationToken cancellationToken)
    {
        return await dbContext
                     .OrganizationInvitations
                     .FirstOrDefaultAsync(invitation =>
                             invitation.OrganizationId == organizationId &&
                             invitation.Email == email &&
                             invitation.AcceptedAt == null &&
                             invitation.RevokedAt == null,
                         cancellationToken);
    }

    public async Task AddAsync(OrganizationInvitation organizationInvitation, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(organizationInvitation, cancellationToken);
    }
}