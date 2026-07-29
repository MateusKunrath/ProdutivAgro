using FluentAssertions;
using ProdutivAgro.Application.Identity.Commands.Login;
using ProdutivAgro.Exception;
using ProdutivAgro.Testing.Common.Identity.Commands.Login;

namespace ProdutivAgro.Application.UnitTests.Identity.Commands.Login;

public class LoginCommandValidatorTests
{
    private readonly LoginCommandValidator _validator = new();

    [Fact]
    public void Success()
    {
        var command = new LoginCommandBuilder().Build();

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorEmailEmpty(string? email)
    {
        var command = new LoginCommandBuilder().WithEmail(email).Build();

        AssertSingleError(command, ResourceErrorMessages.EMAIL_EMPTY);
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("user@")]
    [InlineData("user.com")]
    public void ErrorEmailInvalid(string? email)
    {
        var command = new LoginCommandBuilder().WithEmail(email).Build();

        AssertSingleError(command, ResourceErrorMessages.EMAIL_INVALID);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("weak")]
    public void ErrorPasswordInvalid(string? password)
    {
        var command = new LoginCommandBuilder().WithPassword(password).Build();

        AssertSingleError(command, ResourceErrorMessages.PASSWORD_INVALID);
    }

    private void AssertSingleError(LoginCommand command, string expectedMessage)
    {
        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error => error.ErrorMessage == expectedMessage);
    }
}