using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.UseCases.Users;

public partial class PhoneNumberValidator<T> : PropertyValidator<T, string>
{
    private const string ErrorMessageKey = "ErrorMessage";

    public override string Name => nameof(PhoneNumberValidator<T>);

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ErrorMessageKey}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string phoneNumber)
    {
        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            var digitsOnly = NonDigits().Replace(phoneNumber, "");
            if (ValidPhoneLength().IsMatch(digitsOnly))
            {
                return true;
            }
        }

        context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.PHONE_NUMBER_INVALID);
        return false;
    }

    [GeneratedRegex(@"\D+")]
    private static partial Regex NonDigits();

    [GeneratedRegex(@"^\d{10,11}$")]
    private static partial Regex ValidPhoneLength();
}