using MediatR;
using ProdutivAgro.Application.Sales.Shared.Commands;

namespace ProdutivAgro.Application.Sales.Commands.ReopenSale;

public class ReopenSaleCommand : UpdateSaleStatusCommand<Unit> { }