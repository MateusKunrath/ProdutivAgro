namespace ProdutivAgro.Api.Contracts.Sales;

public sealed class AddSaleItemRequest
{
    public Guid ProductId { get; init; }

    public decimal Quantity { get; init; }
}
