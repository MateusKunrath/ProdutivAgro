using FluentAssertions;
using ProdutivAgro.Application.Products.Commands.CreateProduct;
using ProdutivAgro.Application.Products.Shared.Commands;
using ProdutivAgro.Application.Products.Shared.Validators;
using ProdutivAgro.Exception;
using ProdutivAgro.Testing.Common.Products.Commands;

namespace ProdutivAgro.Application.UnitTests.Products.Commands;

public class ProductCommandValidatorTests
{
    private readonly ProductCommandValidator<CreateProductResult> _validator = new();

    [Fact]
    public void Success()
    {
        var command = new ProductCommandBuilder<CreateProductResult>().Build();

        var result = _validator.Validate(command);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorDescriptionEmpty(string? description)
    {
        var command = new ProductCommandBuilder<CreateProductResult>()
                      .WithDescription(description)
                      .Build();

        AssertSingleError(command, ResourceErrorMessages.PRODUCT_DESCRIPTION_EMPTY);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.01)]
    public void ErrorUnitPriceNotGreaterThanZero(decimal unitPrice)
    {
        var command = new ProductCommandBuilder<CreateProductResult>()
                      .WithUnitPrice(unitPrice)
                      .Build();

        AssertSingleError(command, ResourceErrorMessages.UNIT_PRICE_MUST_BE_GREATER_THAN_ZERO);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("InvalidUnit")]
    public void ErrorMeasurementUnitInvalid(string? measurementUnit)
    {
        var command = new ProductCommandBuilder<CreateProductResult>()
                      .WithMeasurementUnit(measurementUnit)
                      .Build();

        AssertSingleError(command, ResourceErrorMessages.MEASUREMENT_UNIT_INVALID);
    }

    private void AssertSingleError(ProductCommand<CreateProductResult> command, string expectedMessage)
    {
        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error => error.ErrorMessage == expectedMessage);
    }
}