namespace ProdutivAgro.Application.Sales.Commands.CreateSale;

public sealed class SaleItemCommand
{
    public Guid ProductId { get; init; }
    public decimal Quantity { get; init; }
}