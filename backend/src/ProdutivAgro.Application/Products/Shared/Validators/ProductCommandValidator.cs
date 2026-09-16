using FluentValidation;
using ProdutivAgro.Application.Products.Shared.Commands;
using ProdutivAgro.Domain.Products.Enums;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Products.Shared.Validators;

public class ProductCommandValidator<TResult> : AbstractValidator<ProductCommand<TResult>>
{
    public ProductCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage(ResourceErrorMessages.PRODUCT_DESCRIPTION_EMPTY);
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.UNIT_PRICE_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(x => x.MeasurementUnit)
            .Must(value =>
                !string.IsNullOrWhiteSpace(value) &&
                Enum.GetNames<MeasurementUnit>()
                    .Contains(value, StringComparer.OrdinalIgnoreCase))
            .WithMessage(ResourceErrorMessages.MEASUREMENT_UNIT_INVALID);
    }
}