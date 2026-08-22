using MediatR;
using ProdutivAgro.Application.Sales.Shared.Commands;

namespace ProdutivAgro.Application.Sales.Commands.CancelSale;

public sealed class CancelSaleCommand : UpdateSaleStatusCommand<Unit> { }