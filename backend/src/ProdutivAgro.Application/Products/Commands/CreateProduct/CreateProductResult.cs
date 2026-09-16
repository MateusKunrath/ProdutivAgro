namespace ProdutivAgro.Application.Products.Commands.CreateProduct;

public sealed class CreateProductResult
{
    public Guid Id { get; init; }
    public string Description { get; init; } = string.Empty;
}