namespace ProdutivAgro.Api.Contracts.Sales;

public sealed class CreateSaleRequest
{
    public DateTimeOffset SoldAt { get; init; }
}