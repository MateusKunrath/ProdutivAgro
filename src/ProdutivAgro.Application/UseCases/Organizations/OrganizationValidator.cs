using FluentValidation;
using ProdutivAgro.Communication.Requests;

namespace ProdutivAgro.Application.UseCases.Organizations;

public class OrganizationValidator : AbstractValidator<RequestOrganizationJson>
{
    public OrganizationValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Name is required");
    }
}