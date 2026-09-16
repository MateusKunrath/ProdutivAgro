using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Sales.Shared.Commands;

public class UpdateSaleStatusCommand<TResult> : IRequest<TResult>, IRequireActiveOrganization
{
    public Guid Id { get; init; }
    public string Reason { get; init; } = string.Empty;
}