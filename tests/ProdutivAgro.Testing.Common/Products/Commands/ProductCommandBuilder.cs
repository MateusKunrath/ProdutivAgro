using Bogus;
using ProdutivAgro.Application.Products.Shared.Commands;
using ProdutivAgro.Domain.Products.Enums;

namespace ProdutivAgro.Testing.Common.Products.Commands;

public sealed class ProductCommandBuilder<TResult>
{
    private readonly Faker _faker = new();
    private string? _description;
    private string? _measurementUnit;
    private decimal _unitPrice;

    public ProductCommandBuilder()
    {
        _description = _faker.Commerce.ProductName();
        _unitPrice = _faker.Random.Decimal(0.01m, 10_000m);
        _measurementUnit = _faker.PickRandom<MeasurementUnit>().ToString();
    }

    public ProductCommandBuilder<TResult> WithDescription(string? description)
    {
        _description = description;
        return this;
    }

    public ProductCommandBuilder<TResult> WithUnitPrice(decimal unitPrice)
    {
        _unitPrice = unitPrice;
        return this;
    }

    public ProductCommandBuilder<TResult> WithMeasurementUnit(string? measurementUnit)
    {
        _measurementUnit = measurementUnit;
        return this;
    }

    public ProductCommand<TResult> Build()
    {
        return new ProductCommand<TResult>
        {
            Description = _description!,
            UnitPrice = _unitPrice,
            MeasurementUnit = _measurementUnit!,
        };
    }
}