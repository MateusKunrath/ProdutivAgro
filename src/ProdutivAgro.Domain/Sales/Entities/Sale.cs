using ProdutivAgro.Domain.Products.Entities;
using ProdutivAgro.Domain.Sales.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Sales.Entities;

public class Sale : AggregateRoot
{
    private readonly List<SaleItem> _items = [];

    public Guid OrganizationId { get; private set; }

    public Guid CreatedByUserId { get; private set; }
    public Guid? UpdatedByUserId { get; private set; }

    public SaleStatus Status { get; private set; }

    public decimal TotalAmount { get; private set; }

    public DateTimeOffset SoldAt { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public IReadOnlyCollection<SaleItem> Items => _items;

    public void AddItem(Product product, decimal quantity)
    {
        var item = new SaleItem(
            Id,
            product.Id,
            product.Description,
            product.Unit,
            quantity,
            product.UnitPrice);

        _items.Add(item);

        TotalAmount = _items.Sum(x => x.TotalAmount);
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}