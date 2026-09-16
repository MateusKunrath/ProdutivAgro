using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Products.Queries.GetProductById;

public sealed class GetProductByIdQuery : IRequest<GetProductByIdResult>, IRequireActiveOrganization
{
    public Guid Id { get; init; }
}