using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommand : IRequest<CreateProductResult>, IRequireActiveOrganization
{
    public string Description { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public string MeasurementUnit { get; init; } = string.Empty;
}
