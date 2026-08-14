using FluentValidation;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Sales.Commands.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.Items).NotEmpty().WithMessage(ResourceErrorMessages.SALE_ITEMS_EMPTY);
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.PRODUCT_ID_IS_REQUIRED);

            item.RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage(ResourceErrorMessages.QUANTITY_MUST_BE_GREATER_THAN_ZERO);
        });
    }
}