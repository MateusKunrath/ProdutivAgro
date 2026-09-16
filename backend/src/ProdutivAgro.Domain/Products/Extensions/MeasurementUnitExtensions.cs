using ProdutivAgro.Domain.Products.Enums;

namespace ProdutivAgro.Domain.Products.Extensions;

public static class MeasurementUnitExtensions
{
    public static string MeasurementUnitToString(this MeasurementUnit measurementUnit)
    {
        return measurementUnit switch
        {
            MeasurementUnit.Unit => nameof(MeasurementUnit.Unit),
            MeasurementUnit.Box => nameof(MeasurementUnit.Box),
            MeasurementUnit.Kilogram => nameof(MeasurementUnit.Kilogram),
            MeasurementUnit.Tray => nameof(MeasurementUnit.Tray),
            _ => string.Empty,
        };
    }
}