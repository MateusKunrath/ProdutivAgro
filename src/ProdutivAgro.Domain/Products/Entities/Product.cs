using ProdutivAgro.Domain.Products.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Products.Entities;

public class Product : AggregateRoot
{
    public Product(Guid organizationId, string description, decimal unitPrice, MeasurementUnit unit)
    {
        var now = DateTimeOffset.UtcNow;

        Description = description;
        OrganizationId = organizationId;
        UnitPrice = unitPrice;
        Unit = unit;
        Active = true;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public Guid OrganizationId { get; private set; }
    public string Description { get; private set; }
    public decimal UnitPrice { get; private set; }
    public MeasurementUnit Unit { get; private set; }
    public bool Active { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public void Update(string description, decimal unitPrice, MeasurementUnit unit)
    {
        Description = description;
        UnitPrice = unitPrice;
        Unit = unit;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
