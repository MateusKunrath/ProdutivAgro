using CommonTestUtilities.Requests;
using FluentAssertions;
using ProdutivAgro.Application.UseCases.Users.Create;
using ProdutivAgro.Exception;

namespace Validators.Tests.Users.Create;

public class CreateUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new CreateUserValidator();
        var request = RequestCreateUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorNameEmpty(string? name)
    {
        var validator = new CreateUserValidator();
        var request = RequestCreateUserJsonBuilder.Build();
        request.Name = name;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And
              .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.NAME_EMPTY));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorEmailEmpty(string? email)
    {
        var validator = new CreateUserValidator();
        var request = RequestCreateUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And
              .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_EMPTY));
    }

    [Fact]
    public void ErrorEmailInvalid()
    {
        var validator = new CreateUserValidator();
        var request = RequestCreateUserJsonBuilder.Build();
        request.Email = "teste.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And
              .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.EMAIL_INVALID));
    }

    [Fact]
    public void ErrorPhoneNumberInvalid()
    {
        var validator = new CreateUserValidator();
        var request = RequestCreateUserJsonBuilder.Build();
        request.PhoneNumber = "555555555555555555555";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And
              .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PHONE_NUMBER_INVALID));
    }

    [Fact]
    public void ErrorPasswordEmpty()
    {
        var validator = new CreateUserValidator();
        var request = RequestCreateUserJsonBuilder.Build();
        request.Password = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And
              .Contain(error => error.ErrorMessage.Equals(ResourceErrorMessages.PASSWORD_INVALID));
    }
}