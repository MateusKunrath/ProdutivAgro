using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<Unit>, IRequireActiveOrganization
{
    public Guid Id { get; init; }
}