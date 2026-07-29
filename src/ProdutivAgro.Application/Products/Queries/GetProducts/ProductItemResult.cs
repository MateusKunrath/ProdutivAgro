namespace ProdutivAgro.Application.Products.Queries.GetProducts;

public sealed class ProductItemResult
{
    public Guid Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public bool Active { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}