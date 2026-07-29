using FluentAssertions;
using ProdutivAgro.Application.Identity.Commands.ChangeOrganizationResponsible;
using ProdutivAgro.Exception;
using ProdutivAgro.Testing.Common.Identity.Commands.ChangeOrganizationResponsible;

namespace ProdutivAgro.Application.UnitTests.Identity.Commands.ChangeOrganizationResponsible;

public class ChangeOrganizationResponsibleCommandValidatorTests
{
    private readonly ChangeOrganizationResponsibleCommandValidator _validator = new();

    [Fact]
    public void Should_be_valid_when_new_responsible_user_id_is_provided()
    {
        var command = new ChangeOrganizationResponsibleCommandBuilder().Build();

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_have_error_when_new_responsible_user_id_is_empty()
    {
        var command = new ChangeOrganizationResponsibleCommandBuilder()
                      .WithNewResponsibleUserId(Guid.Empty)
                      .Build();

        AssertSingleError(command, ResourceErrorMessages.RESPONSIBLE_EMPTY);
    }

    private void AssertSingleError(ChangeOrganizationResponsibleCommand command, string expectedMessage)
    {
        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error => error.ErrorMessage == expectedMessage);
    }
}
