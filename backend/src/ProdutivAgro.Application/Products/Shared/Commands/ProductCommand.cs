using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Products.Shared.Commands;

public class ProductCommand<TResult> : IRequest<TResult>, IRequireActiveOrganization
{
    public string Description { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public string MeasurementUnit { get; init; } = string.Empty;
}
