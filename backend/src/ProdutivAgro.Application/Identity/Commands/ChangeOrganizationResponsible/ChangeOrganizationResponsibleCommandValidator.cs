using FluentValidation;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.Identity.Commands.ChangeOrganizationResponsible;

public class ChangeOrganizationResponsibleCommandValidator : AbstractValidator<ChangeOrganizationResponsibleCommand>
{
    public ChangeOrganizationResponsibleCommandValidator()
    {
        RuleFor(x => x.NewResponsibleUserId).NotEmpty().WithMessage(ResourceErrorMessages.RESPONSIBLE_EMPTY);
    }
}