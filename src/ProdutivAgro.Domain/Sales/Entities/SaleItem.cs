using ProdutivAgro.Domain.Products.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Sales.Entities;

public class SaleItem : Entity
{
    public SaleItem(
        Guid saleId,
        Guid sourceProductId,
        string productDescription,
        MeasurementUnit unit,
        decimal quantity,
        decimal unitPrice)
    {
        SaleId = saleId;
        SourceProductId = sourceProductId;
        ProductDescription = productDescription;
        Unit = unit;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalAmount = quantity * unitPrice;
    }

    public Guid SaleId { get; private set; }

    public Guid SourceProductId { get; private set; }

    public string ProductDescription { get; private set; }
    public MeasurementUnit Unit { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public decimal TotalAmount { get; private set; }
}