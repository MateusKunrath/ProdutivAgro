using ProdutivAgro.Application.Abstractions.Pagination;

namespace ProdutivAgro.Application.Identity.Queries.GetInvitations;

public sealed class GetInvitationsResult : PagedResult
{
    public List<GetInvitationItemResult> Items { get; init; } = [];
}