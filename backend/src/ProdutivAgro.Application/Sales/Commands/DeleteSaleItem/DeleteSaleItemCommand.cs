using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Commands.DeleteSaleItem;

public sealed class DeleteSaleItemCommand : IRequest<Unit>, IRequireActiveOrganization
{
    public Guid SaleId { get; init; }
    public Guid SaleItemId { get; init; }
}