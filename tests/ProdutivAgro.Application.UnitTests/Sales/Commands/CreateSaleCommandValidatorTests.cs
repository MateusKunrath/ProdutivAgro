using FluentAssertions;
using ProdutivAgro.Application.Sales.Commands.CreateSale;
using ProdutivAgro.Exception;

namespace ProdutivAgro.Application.UnitTests.Sales.Commands;

public class CreateSaleCommandValidatorTests
{
    private readonly CreateSaleCommandValidator _validator = new();

    [Fact]
    public void ErrorSoldAtInFuture()
    {
        var command = new CreateSaleCommand
        {
            SoldAt = DateTimeOffset.UtcNow.AddMinutes(1),
        };

        var result = _validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(error =>
            error.ErrorMessage == ResourceErrorMessages.SOLD_AT_CANNOT_BE_IN_THE_FUTURE);
    }
}
