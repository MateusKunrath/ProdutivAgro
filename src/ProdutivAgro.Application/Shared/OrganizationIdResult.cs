namespace ProdutivAgro.Application.Shared;

/// <summary>
/// Represents an organization reference in results returned by other entities.
/// </summary>
public sealed class OrganizationIdResult
{
    /// <summary>
    /// Gets the organization identifier.
    /// </summary>
    public Guid Id { get; init; }
}
