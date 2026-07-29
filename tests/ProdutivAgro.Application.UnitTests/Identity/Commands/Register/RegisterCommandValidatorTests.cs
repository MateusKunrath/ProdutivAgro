using FluentAssertions;
using ProdutivAgro.Application.Identity.Commands.Register;
using ProdutivAgro.Exception;
using ProdutivAgro.Testing.Common.Identity.Commands.Register;

namespace ProdutivAgro.Application.UnitTests.Identity.Commands.Register;

public class RegisterCommandValidatorTests
{
    private readonly RegisterCommandValidator _validator = new();

    [Fact]
    public void Success()
    {
        var command = new RegisterCommandBuilder().Build();

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorNameEmpty(string? name)
    {
        var command = new RegisterCommandBuilder().WithName(name).Build();

        AssertSingleError(command, ResourceErrorMessages.NAME_EMPTY);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorEmailEmpty(string? email)
    {
        var command = new RegisterCommandBuilder().WithEmail(email).Build();

        AssertSingleError(command, ResourceErrorMessages.EMAIL_EMPTY);
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("user@")]
    [InlineData("user.com")]
    public void ErrorEmailInvalid(string email)
    {
        var command = new RegisterCommandBuilder().WithEmail(email).Build();

        AssertSingleError(command, ResourceErrorMessages.EMAIL_INVALID);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("weak")]
    public void ErrorPasswordInvalid(string? password)
    {
        var command = new RegisterCommandBuilder().WithPassword(password).Build();

        AssertSingleError(command, ResourceErrorMessages.PASSWORD_INVALID);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorOrganizationNameEmpty(string? organizationName)
    {
        var command = new RegisterCommandBuilder().WithOrganizationName(organizationName).Build();

        AssertSingleError(command, ResourceErrorMessages.ORGANIZATION_NAME_EMPTY);
    }

    private void AssertSingleError(RegisterCommand command, string expectedMessage)
    {
        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error => error.ErrorMessage == expectedMessage);
    }
}