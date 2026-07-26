namespace ProdutivAgro.Application.Identity.Queries.GetCurrentOrganization;

public sealed class GetCurrentOrganizationResult
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool Active { get; init; }
    public Guid? ResponsibleUserId { get; init; }
}
