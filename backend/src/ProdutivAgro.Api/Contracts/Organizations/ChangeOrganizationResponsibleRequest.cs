namespace ProdutivAgro.Api.Contracts.Organizations;

public sealed class ChangeOrganizationResponsibleRequest
{
    public Guid NewResponsibleUserId { get; init; }
}
