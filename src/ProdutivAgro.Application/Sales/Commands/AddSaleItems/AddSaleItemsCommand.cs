using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Commands.AddSaleItems;

public sealed class AddSaleItemsCommand : IRequest<Unit>, IRequireActiveOrganization
{
    public Guid SaleId { get; init; }

    public List<AddSaleItemCommand> Items { get; init; } = [];
}
