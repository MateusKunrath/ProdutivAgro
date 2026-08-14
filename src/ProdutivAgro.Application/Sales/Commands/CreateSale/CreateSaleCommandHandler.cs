using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Products.Entities;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Domain.Sales.Entities;
using ProdutivAgro.Domain.Sales.Extensions;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Commands.CreateSale;

public class CreateSaleCommandHandler(
    IProductsReadOnlyRepository productsReadOnlyRepository,
    ISalesWriteOnlyRepository salesWriteOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var products = await GetProducts(request.Items, cancellationToken);

        var sale = new Sale(currentUser.OrganizationId, currentUser.UserId, DateTimeOffset.UtcNow);

        foreach (var product in products)
        {
            var productQuantity = request.Items.Where(x => x.ProductId == product.Id).Sum(x => x.Quantity);
            sale.AddItem(product, productQuantity);
        }

        await salesWriteOnlyRepository.AddAsync(sale, cancellationToken);
        await unitOfWork.Commit();

        return new CreateSaleResult
        {
            Id = sale.Id,
            Status = sale.Status.SaleStatusToString(),
            TotalAmount = sale.TotalAmount,
        };
    }

    private async Task<List<Product>> GetProducts(List<SaleItemCommand> saleItems, CancellationToken cancellationToken)
    {
        var productsIds = saleItems.Select(x => x.ProductId).Distinct().ToList();

        var products = await productsReadOnlyRepository
            .GetByIdsAsync(productsIds, currentUser.OrganizationId, cancellationToken);

        var foundIds = products.Select(x => x.Id).ToHashSet();

        return productsIds.Any(id => !foundIds.Contains(id))
            ? throw new NotFoundException(ResourceErrorMessages.PRODUCT_NOT_FOUND)
            : products;
    }

    private async Task Validate(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var result = await new CreateSaleCommandValidator().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}