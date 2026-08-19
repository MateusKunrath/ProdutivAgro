namespace ProdutivAgro.Application.Sales.Commands.AddSaleItems;

public sealed class AddSaleItemCommand
{
    public Guid ProductId { get; init; }

    public decimal Quantity { get; init; }
}
