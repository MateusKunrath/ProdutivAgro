using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Infrastructure.Persistence;

namespace ProdutivAgro.Infrastructure.Identity.Persistence.Repositories;

public class OrganizationInvitationsRepository(ProdutivAgroDbContext dbContext)
    : IInvitationsUpdateOnlyRepository, IInvitationsWriteOnlyRepository, IInvitationsReadOnlyRepository
{
    public async Task<(List<OrganizationInvitation> Items, int TotalCount)> GetPagedAsync(Guid organizationId,
        int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.OrganizationInvitations
                             .AsNoTracking()
                             .Include(invitation => invitation.InvitedByUser)
                             .Where(invitation => invitation.OrganizationId == organizationId)
                             .OrderBy(invitation => invitation.CreatedAt)
                             .ThenBy(invitation => invitation.Id);

        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
                          .Skip((pageNumber - 1) * pageSize)
                          .Take(pageSize)
                          .ToListAsync(cancellationToken);

        return (items, totalItems);
    }

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