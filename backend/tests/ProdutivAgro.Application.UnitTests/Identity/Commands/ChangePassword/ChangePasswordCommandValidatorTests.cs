using FluentAssertions;
using ProdutivAgro.Application.Identity.Commands.ChangePassword;
using ProdutivAgro.Exception;
using ProdutivAgro.Testing.Common.Identity.Commands.ChangePassword;

namespace ProdutivAgro.Application.UnitTests.Identity.Commands.ChangePassword;

public class ChangePasswordCommandValidatorTests
{
    private readonly ChangePasswordCommandValidator _validator = new();

    [Fact]
    public void Success()
    {
        var command = new ChangePasswordCommandBuilder().Build();

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorCurrentPasswordInvalid(string? currentPassword)
    {
        var command = new ChangePasswordCommandBuilder().WithCurrentPassword(currentPassword).Build();

        AssertSingleError(command, ResourceErrorMessages.CURRENT_PASSWORD_EMPTY);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorNewPasswordInvalid(string? newPassword)
    {
        var command = new ChangePasswordCommandBuilder().WithNewPassword(newPassword).Build();

        AssertSingleError(command, ResourceErrorMessages.PASSWORD_INVALID);
    }

    private void AssertSingleError(ChangePasswordCommand command, string expectedMessage)
    {
        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error => error.ErrorMessage == expectedMessage);
    }
}