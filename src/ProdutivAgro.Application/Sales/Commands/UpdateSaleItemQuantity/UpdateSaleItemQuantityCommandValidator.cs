using FluentValidation;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Sales.Commands.UpdateSaleItemQuantity;

public sealed class UpdateSaleItemQuantityCommandValidator : AbstractValidator<UpdateSaleItemQuantityCommand>
{
    public UpdateSaleItemQuantityCommandValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.QUANTITY_MUST_BE_GREATER_THAN_ZERO);
    }
}