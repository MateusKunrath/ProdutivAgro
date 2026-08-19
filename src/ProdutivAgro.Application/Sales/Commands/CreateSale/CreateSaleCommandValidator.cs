using FluentValidation;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Sales.Commands.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.SoldAt)
            .NotEmpty().WithMessage(ResourceErrorMessages.SOLD_AT_EMPTY)
            .LessThanOrEqualTo(DateTimeOffset.UtcNow)
            .WithMessage(ResourceErrorMessages.SOLD_AT_CANNOT_BE_IN_THE_FUTURE);
    }
}
