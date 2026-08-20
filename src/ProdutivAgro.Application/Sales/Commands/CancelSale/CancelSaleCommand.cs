using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Commands.CancelSale;

public sealed class CancelSaleCommand : IRequest<Unit>, IRequireActiveOrganization
{
    public Guid Id { get; init; }
}