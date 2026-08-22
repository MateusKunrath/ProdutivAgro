using FluentValidation;
using ProdutivAgro.Application.Sales.Shared.Commands;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Sales.Shared.Validators;

public class UpdateSaleStatusValidator<TResult> : AbstractValidator<UpdateSaleStatusCommand<TResult>>
{
    public UpdateSaleStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(ResourceErrorMessages.ID_IS_REQUIRED);
        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(ResourceErrorMessages.REASON_IS_REQUIRED)
            .MinimumLength(5).WithMessage(ResourceErrorMessages.REASON_INVALID);
    }
}