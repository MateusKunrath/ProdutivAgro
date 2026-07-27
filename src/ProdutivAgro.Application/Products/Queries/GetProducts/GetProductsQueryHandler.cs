using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler(
    IOrganizationsReadOnlyRepository organizationsReadOnlyRepository,
    IProductsReadOnlyRepository productsReadOnlyRepository,
    ICurrentUser currentUser) : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var organization =
            await organizationsReadOnlyRepository.GetByIdAsync(currentUser.OrganizationId, cancellationToken);
        if (organization is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ORGANIZATION_NOT_FOUND);
        }

        var pageNumber = Math.Max(request.PageNumber, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var (products, totalCount) = await productsReadOnlyRepository.GetPagedAsync(
            currentUser.OrganizationId,
            pageNumber,
            pageSize,
            cancellationToken);

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new GetProductsResult
        {
            Items =
            [
                .. products.Select(product => new ProductItemResult
                {
                    Id = product.Id,
                    Description = product.Description,
                    UnitPrice = product.UnitPrice,
                    Active = product.Active,
                }),
            ],
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
        };
    }
}