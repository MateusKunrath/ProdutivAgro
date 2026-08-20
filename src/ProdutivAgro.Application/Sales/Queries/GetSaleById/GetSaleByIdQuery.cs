using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Queries.GetSaleById;

public sealed class GetSaleByIdQuery : IRequest<GetSaleByIdResult>, IRequireActiveOrganization
{
    public Guid Id { get; init; }
}