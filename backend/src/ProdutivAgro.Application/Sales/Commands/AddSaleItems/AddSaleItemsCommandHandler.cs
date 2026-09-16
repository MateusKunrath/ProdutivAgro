using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Products.Entities;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Domain.Sales.Entities;
using ProdutivAgro.Domain.Sales.Enums;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Commands.AddSaleItems;

public sealed class AddSaleItemsCommandHandler(
    ISalesUpdateOnlyRepository salesUpdateOnlyRepository,
    ISalesWriteOnlyRepository salesWriteOnlyRepository,
    IProductsReadOnlyRepository productsReadOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<AddSaleItemsCommand, List<AddSaleItemResult>>
{
    public async Task<List<AddSaleItemResult>> Handle(AddSaleItemsCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var sale = await salesUpdateOnlyRepository.GetByIdAsync(
            request.SaleId,
            currentUser.OrganizationId,
            cancellationToken);

        if (sale is null)
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_NOT_FOUND);
        }

        if (sale.Status != SaleStatus.Draft)
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.SALE_STATUS_INVALID]);
        }

        var products = await GetProducts(request.Items, cancellationToken);
        var productsById = products.ToDictionary(product => product.Id);

        var saleItems = request.Items
            .Select(item => sale.AddItem(productsById[item.ProductId], item.Quantity))
            .ToList();

        await salesWriteOnlyRepository.AddItemsAsync(saleItems, cancellationToken);
        await unitOfWork.Commit();

        return saleItems.Select(item => new AddSaleItemResult
        {
            ProductId = item.SourceProductId,
            SaleItemId = item.Id,
        }).ToList();
    }

    private async Task<List<Product>> GetProducts(
        List<AddSaleItemCommand> saleItems,
        CancellationToken cancellationToken)
    {
        var productIds = saleItems.Select(item => item.ProductId).Distinct().ToList();
        var products = await productsReadOnlyRepository.GetByIdsAsync(
            productIds,
            currentUser.OrganizationId,
            cancellationToken);

        var foundIds = products.Select(product => product.Id).ToHashSet();
        if (productIds.Any(productId => !foundIds.Contains(productId)))
        {
            throw new NotFoundException(ResourceErrorMessages.PRODUCT_NOT_FOUND);
        }

        return products;
    }

    private static async Task Validate(AddSaleItemsCommand request, CancellationToken cancellationToken)
    {
        var result = await new AddSaleItemsCommandValidator().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ErrorOnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());
        }
    }
}
