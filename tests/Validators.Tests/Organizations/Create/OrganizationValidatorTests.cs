using CommonTestUtilities.Requests;
using FluentAssertions;
using ProdutivAgro.Application.UseCases.Organizations;
using ProdutivAgro.Exception;

namespace Validators.Tests.Organizations.Create;

public class OrganizationValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new OrganizationValidator();
        var request = RequestOrganizationJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorOrganizationNameRequired(string organizationName)
    {
        var validator = new OrganizationValidator();
        var request = RequestOrganizationJsonBuilder.Build();
        request.Name = organizationName;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.Contain(error =>
            error.ErrorMessage.Equals(ResourceErrorMessages.ORGANIZATION_NAME_EMPTY));
    }
}