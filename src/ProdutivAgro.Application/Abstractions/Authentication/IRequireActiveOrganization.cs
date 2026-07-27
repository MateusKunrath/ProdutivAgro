namespace ProdutivAgro.Application.Abstractions.Authentication;

/// <summary>
/// Marks a MediatR request that can only run for an existing, active organization.
/// </summary>
public interface IRequireActiveOrganization;
