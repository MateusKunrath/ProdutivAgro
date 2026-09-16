using ProdutivAgro.Domain.Sales.Enums;

namespace ProdutivAgro.Domain.Sales.Extensions;

public static class SaleStatusExtension
{
    public static string SaleStatusToString(this SaleStatus saleStatus)
    {
        return saleStatus switch
        {
            SaleStatus.Draft => nameof(SaleStatus.Draft),
            SaleStatus.Completed => nameof(SaleStatus.Completed),
            SaleStatus.Cancelled => nameof(SaleStatus.Cancelled),
            _ => string.Empty,
        };
    }
}