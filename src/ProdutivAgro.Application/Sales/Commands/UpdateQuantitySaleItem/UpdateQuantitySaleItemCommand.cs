using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Commands.UpdateQuantitySaleItem;

public sealed class UpdateQuantitySaleItemCommand : IRequest<Unit>, IRequireActiveOrganization
{
    public Guid Id { get; init; }
    public Guid SaleItemId { get; init; }
    public decimal Quantity { get; init; }
}