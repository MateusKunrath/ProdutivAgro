using ProdutivAgro.Application.Sales.Commands.CreateSale;

namespace ProdutivAgro.Api.Contracts.Sales;

public sealed class CreateSaleRequest
{
    public List<SaleItemCommand> Items { get; init; } = [];
}