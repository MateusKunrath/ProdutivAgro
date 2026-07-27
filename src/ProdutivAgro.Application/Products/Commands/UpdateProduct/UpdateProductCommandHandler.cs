using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Application.Products.Shared.Validators;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Domain.Products.Enums;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductsUpdateOnlyRepository productsUpdateOnlyRepository,
    IOrganizationsReadOnlyRepository organizationsReadOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var organizationExists =
            await organizationsReadOnlyRepository.ExistsAndIsActiveAsync(currentUser.OrganizationId, cancellationToken);
        if (!organizationExists)
        {
            throw new NotFoundException(ResourceErrorMessages.ORGANIZATION_NOT_FOUND);
        }

        await Validate(request, cancellationToken);

        var product = await productsUpdateOnlyRepository.GetByIdAsync(
            request.Id,
            currentUser.OrganizationId,
            cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(ResourceErrorMessages.PRODUCT_NOT_FOUND);
        }

        var measurementUnit = Enum.Parse<MeasurementUnit>(request.MeasurementUnit, true);
        product.Update(request.Description, request.UnitPrice, measurementUnit);

        productsUpdateOnlyRepository.Update(product);

        await unitOfWork.Commit();
        return Unit.Value;
    }

    private async Task Validate(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await new ProductCommandValidator<Unit>().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}