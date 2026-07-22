using Bogus;
using FluentAssertions;
using FluentValidation;
using ProdutivAgro.Application.UseCases.Users;
using ProdutivAgro.Communication.Requests.Users;

namespace Validators.Tests.Users;

public class PhoneNumberValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    [InlineData("1234567")]
    [InlineData("12345678")]
    [InlineData("123456789")]
    [InlineData("123456789012")]
    [InlineData("abcdefghij")]
    [InlineData("(11) abcd-5678")]
    public void ErrorPhoneNumberInvalid(string? phoneNumber)
    {
        var validator = new PhoneNumberValidator<RequestCreateUserJson>();

        var result = validator.IsValid(
            new ValidationContext<RequestCreateUserJson>(new RequestCreateUserJson()),
            phoneNumber
        );

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("##########")]
    [InlineData("###########")]
    public void SuccessPhoneNumberValid(string format)
    {
        var faker = new Faker("pt_BR");
        var phoneNumber = faker.Phone.PhoneNumber(format);

        var validator = new PhoneNumberValidator<RequestCreateUserJson>();

        var result = validator.IsValid(
            new ValidationContext<RequestCreateUserJson>(new RequestCreateUserJson()),
            phoneNumber
        );

        result.Should().BeTrue();
    }
}