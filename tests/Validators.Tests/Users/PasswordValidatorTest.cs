using FluentAssertions;
using FluentValidation;
using ProdutivAgro.Application.UseCases.Users;
using ProdutivAgro.Communication.Requests.Users;

namespace Validators.Tests.Users;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("AAAAAAAa")]
    [InlineData("AAAAAaA1")]
    public void ErrorPasswordInvalid(string? password)
    {
        var validator = new PasswordValidator<RequestCreateUserJson>();

        var result = validator.IsValid(
            new ValidationContext<RequestCreateUserJson>(new RequestCreateUserJson()),
            password
        );

        result.Should().BeFalse();
    }
}