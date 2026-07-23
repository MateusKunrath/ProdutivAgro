using ProdutivAgro.Domain.Products.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Products.Entities;

public class Product : AggregateRoot
{
    public Guid OrganizationId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public MeasurementUnit Unit { get; private set; }
    public bool Active { get; private set; }
}