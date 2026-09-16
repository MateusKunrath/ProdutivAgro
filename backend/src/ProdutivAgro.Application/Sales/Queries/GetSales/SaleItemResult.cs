using ProdutivAgro.Application.Shared;

namespace ProdutivAgro.Application.Sales.Queries.GetSales;

public class SaleItemResult
{
    public Guid Id { get; init; }
    public decimal TotalAmount { get; init; }
    public string Status { get; init; } = string.Empty;
    public UserResult SaleUser { get; init; } = new();
    public OrganizationIdResult Organization { get; init; } = new();
    public DateTimeOffset SoldAt { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}