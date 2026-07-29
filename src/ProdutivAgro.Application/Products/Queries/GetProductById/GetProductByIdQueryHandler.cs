using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Domain.Products.Extensions;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(
    IProductsReadOnlyRepository productsReadOnlyRepository,
    ICurrentUser currentUser) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productsReadOnlyRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null || !product.OrganizationId.Equals(currentUser.OrganizationId))
        {
            throw new NotFoundException(ResourceErrorMessages.PRODUCT_NOT_FOUND);
        }

        return new GetProductByIdResult
        {
            Id = product.Id,
            Description = product.Description,
            MeasurementUnit = product.Unit.MeasurementUnitToString(),
            UnitPrice = product.UnitPrice,
        };
    }
}