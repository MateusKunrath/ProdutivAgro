namespace ProdutivAgro.Application.Sales.Commands.AddSaleItems;

public sealed class AddSaleItemResult
{
    public Guid ProductId { get; init; }

    public Guid SaleItemId { get; init; }
}
