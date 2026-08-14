using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Queries.GetSales;

public sealed record GetSalesQuery(
    int PageNumber = 1,
    int PageSize = 20) : IRequest<GetSalesResult>, IRequireActiveOrganization;