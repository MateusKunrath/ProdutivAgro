namespace ProdutivAgro.Application.Sales.Queries.GetSales;

public class SaleItemResult
{
    public Guid Id { get; init; }
    public decimal TotalAmount { get; init; }
    public string Status { get; init; }
    public CreatedByUserResult CreatedByUser { get; init; }
    public Guid OrganizationId { get; init; }
    public DateTimeOffset SoldAt { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}