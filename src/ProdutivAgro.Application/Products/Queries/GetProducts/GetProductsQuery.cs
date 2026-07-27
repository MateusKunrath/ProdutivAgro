using MediatR;

namespace ProdutivAgro.Application.Products.Queries.GetProducts;

public sealed record GetProductsQuery(
    int PageNumber = 1,
    int PageSize = 20
) : IRequest<GetProductsResult>;