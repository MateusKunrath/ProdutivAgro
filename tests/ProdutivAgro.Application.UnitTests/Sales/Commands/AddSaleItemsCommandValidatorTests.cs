using FluentAssertions;
using ProdutivAgro.Application.Sales.Commands.AddSaleItems;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.UnitTests.Sales.Commands;

public class AddSaleItemsCommandValidatorTests
{
    private readonly AddSaleItemsCommandValidator _validator = new();

    [Fact]
    public void ErrorItemsEmpty()
    {
        var result = _validator.Validate(new AddSaleItemsCommand());

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error =>
            error.ErrorMessage == ResourceErrorMessages.SALE_ITEMS_EMPTY);
    }

    [Fact]
    public void ErrorProductIdEmpty()
    {
        var command = new AddSaleItemsCommand
        {
            Items = [new AddSaleItemCommand { Quantity = 1 }],
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error =>
            error.ErrorMessage == ResourceErrorMessages.PRODUCT_ID_IS_REQUIRED);
    }

    [Fact]
    public void ErrorQuantityNotPositive()
    {
        var command = new AddSaleItemsCommand
        {
            Items = [new AddSaleItemCommand { ProductId = Guid.NewGuid() }],
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error =>
            error.ErrorMessage == ResourceErrorMessages.QUANTITY_MUST_BE_GREATER_THAN_ZERO);
    }
}
