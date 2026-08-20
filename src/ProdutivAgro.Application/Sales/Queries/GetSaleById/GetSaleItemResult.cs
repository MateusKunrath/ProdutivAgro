namespace ProdutivAgro.Application.Sales.Queries.GetSaleById;

public class GetSaleItemResult
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string Description { get; init; } = string.Empty;
    public string MeasurementUnit { get; set; } = string.Empty;
    public decimal Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalAmount { get; init; }
}