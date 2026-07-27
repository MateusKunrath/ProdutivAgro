using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Application.Products.Shared.Commands;
using ProdutivAgro.Application.Products.Shared.Validators;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Domain.Products.Entities;
using ProdutivAgro.Domain.Products.Enums;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IProductsWriteOnlyRepository productsWriteOnlyRepository,
    IOrganizationsReadOnlyRepository organizationsReadOnlyRepository,
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser) : IRequestHandler<ProductCommand<CreateProductResult>, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(ProductCommand<CreateProductResult> request,
        CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var organization =
            await organizationsReadOnlyRepository.GetByIdAsync(currentUser.OrganizationId, cancellationToken);
        if (organization is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ORGANIZATION_NOT_FOUND);
        }

        var unit = Enum.Parse<MeasurementUnit>(request.MeasurementUnit, true);
        var product = new Product(
            organization.Id,
            request.Description,
            request.UnitPrice,
            unit);


        await productsWriteOnlyRepository.AddAsync(product, cancellationToken);
        await unitOfWork.Commit();

        return new CreateProductResult
        {
            Id = product.Id,
            Description = product.Description,
        };
    }

    private async Task Validate(ProductCommand<CreateProductResult> request, CancellationToken cancellationToken)
    {
        var result = await new ProductCommandValidator<CreateProductResult>().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}