using FluentValidation;
using ProdutivAgro.Application.Identity.Shared.Validators;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Identity.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);
        RuleFor(x => x.Password).SetValidator(new PasswordValidator<LoginCommand>());
    }
}