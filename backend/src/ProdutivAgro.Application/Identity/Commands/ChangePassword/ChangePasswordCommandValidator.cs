using FluentValidation;
using ProdutivAgro.Application.Identity.Shared.Validators;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Identity.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage(ResourceErrorMessages.CURRENT_PASSWORD_EMPTY);
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<ChangePasswordCommand>());
    }
}