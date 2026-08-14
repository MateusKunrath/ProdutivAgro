using ProdutivAgro.Application.Abstractions.Pagination;

namespace ProdutivAgro.Application.Products.Queries.GetProducts;

public sealed class GetProductsResult : PagedResult
{
    public List<ProductItemResult> Items { get; init; } = [];
}
