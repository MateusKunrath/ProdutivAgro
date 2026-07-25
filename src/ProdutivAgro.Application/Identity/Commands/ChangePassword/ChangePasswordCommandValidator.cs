using FluentValidation;
using ProdutivAgro.Application.Identity.Shared.Validators;

namespace ProdutivAgro.Application.Identity.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty();
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<ChangePasswordCommand>());
    }
}