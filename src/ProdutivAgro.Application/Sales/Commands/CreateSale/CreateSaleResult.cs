namespace ProdutivAgro.Application.Sales.Commands.CreateSale;

public sealed class CreateSaleResult
{
    public Guid Id { get; init; }
    public decimal TotalAmount { get; init; }
    public string Status { get; init; } = string.Empty;
}