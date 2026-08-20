using FluentValidation;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Sales.Commands.UpdateQuantitySaleItem;

public sealed class UpdateQuantitySaleItemCommandValidator : AbstractValidator<UpdateQuantitySaleItemCommand>
{
    public UpdateQuantitySaleItemCommandValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.QUANTITY_MUST_BE_GREATER_THAN_ZERO);
    }
}