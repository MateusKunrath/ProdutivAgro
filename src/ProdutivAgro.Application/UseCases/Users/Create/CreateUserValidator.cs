using FluentValidation;
using ProdutivAgro.Communication.Requests.Users;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.UseCases.Users.Create;

public class CreateUserValidator : AbstractValidator<RequestCreateUserJson>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .When(user => !string.IsNullOrEmpty(user.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);
        RuleFor(x => x.PhoneNumber).SetValidator(new PhoneNumberValidator<RequestCreateUserJson>());
        RuleFor(x => x.Password).SetValidator(new PasswordValidator<RequestCreateUserJson>());
    }
}