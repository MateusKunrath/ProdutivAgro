using FluentValidation;
using ProdutivAgro.Application.Identity.Shared.Validators;
using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Identity.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);
        RuleFor(x => x.Password).SetValidator(new PasswordValidator<RegisterCommand>());
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("ResourceErrorMessages.ROLE_EMPTY")
            .Must(role => Enum.TryParse<UserRole>(role, out _))
            .When(user => !string.IsNullOrWhiteSpace(user.Role), ApplyConditionTo.CurrentValidator)
            .WithMessage("ResourceErrorMessages.ROLE_INVALID");
    }
}