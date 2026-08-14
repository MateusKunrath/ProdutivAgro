using ProdutivAgro.Application.Abstractions.Pagination;

namespace ProdutivAgro.Application.Sales.Queries.GetSales;

public sealed class GetSalesResult : PagedResult
{
    public List<SaleItemResult> Items { get; init; } = [];
}