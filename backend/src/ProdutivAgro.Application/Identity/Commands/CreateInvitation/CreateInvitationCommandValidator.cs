using FluentValidation;
using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Identity.Commands.CreateInvitation;

public class CreateInvitationCommandValidator : AbstractValidator<CreateInvitationCommand>
{
    public CreateInvitationCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);
        RuleFor(x => x.Role)
            .Must(value =>
                !string.IsNullOrWhiteSpace(value) &&
                Enum.GetNames<UserRole>()
                    .Contains(value, StringComparer.OrdinalIgnoreCase))
            .WithMessage(ResourceErrorMessages.USER_ROLE_INVALID);
    }
}