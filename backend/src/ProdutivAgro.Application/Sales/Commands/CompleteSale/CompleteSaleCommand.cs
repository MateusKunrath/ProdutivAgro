using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Commands.CompleteSale;

public sealed class CompleteSaleCommand : IRequest<Unit>, IRequireActiveOrganization
{
    public Guid Id { get; init; }
}