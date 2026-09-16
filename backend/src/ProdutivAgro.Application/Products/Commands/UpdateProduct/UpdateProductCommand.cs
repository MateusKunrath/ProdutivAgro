using MediatR;
using ProdutivAgro.Application.Products.Shared.Commands;

namespace ProdutivAgro.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommand : ProductCommand<Unit>
{
    public Guid Id { get; init; }
}
