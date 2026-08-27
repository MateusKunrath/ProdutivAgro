using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Shared;
using ProdutivAgro.Domain.Identity.Extensions;
using ProdutivAgro.Domain.Identity.Repositories;

namespace ProdutivAgro.Application.Identity.Queries.GetInvitations;

public sealed class GetInvitationsQueryHandler(
    IInvitationsReadOnlyRepository invitationsReadOnlyRepository,
    ICurrentUser currentUser) : IRequestHandler<GetInvitationsQuery, GetInvitationsResult>
{
    public async Task<GetInvitationsResult> Handle(GetInvitationsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(request.PageNumber, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var (invitations, totalCount) = await invitationsReadOnlyRepository.GetPagedAsync(
            currentUser.OrganizationId,
            pageNumber,
            pageSize,
            cancellationToken);

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new GetInvitationsResult
        {
            Items =
            [
                .. invitations.Select(invitation => new GetInvitationItemResult
                {
                    Id = invitation.Id,
                    Email = invitation.Email,
                    InvitedByUser = new UserResult
                    {
                        Id = invitation.InvitedByUser.Id,
                        Name = invitation.InvitedByUser.Name,
                        Email = invitation.InvitedByUser.Email,
                    },
                    Role = invitation.Role.RoleToString(),
                    Organization = new OrganizationIdResult
                    {
                        Id = invitation.OrganizationId,
                    },
                    CreatedAt = invitation.CreatedAt,
                    AcceptedAt = invitation.AcceptedAt,
                    ExpiresAt = invitation.ExpiresAt,
                    RevokedAt = invitation.RevokedAt,
                }),
            ],
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
        };
    }
}