using FluentValidation;
using ProdutivAgro.Application.Identity.Shared.Validators;

namespace ProdutivAgro.Application.Identity.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword).SetValidator(new PasswordValidator<ChangePasswordCommand>());
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<ChangePasswordCommand>());
    }
}