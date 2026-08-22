using MediatR;
using ProdutivAgro.Application.Sales.Shared.Commands;

namespace ProdutivAgro.Application.Sales.Commands.UndoCancellation;

public sealed class UndoCancellationSaleCommand : UpdateSaleStatusCommand<Unit> { }