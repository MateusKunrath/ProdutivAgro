namespace ProdutivAgro.Api.Contracts.Products;

public sealed class UpdateProductRequest
{
    public string Description { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public string MeasurementUnit { get; init; } = string.Empty;
}
