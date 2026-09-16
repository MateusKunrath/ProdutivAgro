using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Identity.Queries.GetInvitations;

public sealed record GetInvitationsQuery(
    int PageNumber = 1,
    int PageSize = 20) : IRequest<GetInvitationsResult>, IRequireActiveOrganization;