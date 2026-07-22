using FluentValidation;
using ProdutivAgro.Communication.Requests;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.UseCases.Organizations;

public class OrganizationValidator : AbstractValidator<RequestOrganizationJson>
{
    public OrganizationValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.ORGANIZATION_NAME_EMPTY);
    }
}