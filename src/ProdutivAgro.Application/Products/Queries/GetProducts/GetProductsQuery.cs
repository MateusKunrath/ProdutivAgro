using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Products.Queries.GetProducts;

public sealed record GetProductsQuery(
    int PageNumber = 1,
    int PageSize = 20
) : IRequest<GetProductsResult>, IRequireActiveOrganization;
