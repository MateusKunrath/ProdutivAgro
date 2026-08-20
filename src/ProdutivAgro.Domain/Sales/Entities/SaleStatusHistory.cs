using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Sales.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Sales.Entities;

public class SaleStatusHistory : Entity
{
    public Guid SaleId { get; private set; }
    public SaleStatus PreviousStatus { get; private set; }
    public SaleStatus CurrentStatus { get; private set; }
    public Guid ChangedByUserId { get; private set; }
    public DateTimeOffset ChangedAt { get; private set; }
    public string? Reason { get; private set; }

    public Sale Sale { get; private set; } = null!;
    public User ChangedByUser { get; private set; } = null!;
}