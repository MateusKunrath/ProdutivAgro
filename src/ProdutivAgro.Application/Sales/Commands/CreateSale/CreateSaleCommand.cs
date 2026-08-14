using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Commands.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>, IRequireActiveOrganization
{
    public List<SaleItemCommand> Items { get; init; } = [];
}