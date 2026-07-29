namespace ProdutivAgro.Application.Products.Queries.GetProductById;

public sealed class GetProductByIdResult
{
    public Guid Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public string MeasurementUnit { get; init; } = string.Empty;
}