using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Commands.AddSaleItems;

public sealed class AddSaleItemsCommand : IRequest<List<AddSaleItemResult>>, IRequireActiveOrganization
{
    public Guid SaleId { get; init; }

    public List<AddSaleItemCommand> Items { get; init; } = [];
}
