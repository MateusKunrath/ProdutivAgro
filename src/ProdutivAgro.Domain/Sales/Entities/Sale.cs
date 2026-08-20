using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Products.Entities;
using ProdutivAgro.Domain.Sales.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Sales.Entities;

public class Sale : AggregateRoot
{
    private readonly List<SaleItem> _items = [];

    public Sale(
        Guid organizationId,
        Guid createdByUserId,
        DateTimeOffset soldAt)
    {
        OrganizationId = organizationId;
        CreatedByUserId = createdByUserId;
        SoldAt = soldAt;
        Status = SaleStatus.Draft;
        TotalAmount = 0;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public Guid OrganizationId { get; private set; }

    public Guid CreatedByUserId { get; private set; }
    public Guid? UpdatedByUserId { get; private set; }
    public User CreatedByUser { get; private set; } = null!;
    public User? UpdatedByUser { get; private set; }

    public SaleStatus Status { get; private set; }

    public decimal TotalAmount { get; private set; }

    public DateTimeOffset SoldAt { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public IReadOnlyCollection<SaleItem> Items => _items;

    public SaleItem AddItem(Product product, decimal quantity)
    {
        var item = new SaleItem(
            Id,
            product.Id,
            product.Description,
            product.Unit,
            quantity,
            product.UnitPrice);

        _items.Add(item);

        UpdateTotalAmount();
        Touched();

        return item;
    }

    public void SetSaleStatus(SaleStatus status)
    {
        Status = status;
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = _items.Sum(x => x.TotalAmount);
    }

    private void Touched()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public bool UpdateItemQuantity(Guid saleItemId, decimal quantity)
    {
        var item = _items.FirstOrDefault(x => x.Id == saleItemId);
        if (item is null)
        {
            return false;
        }

        item.UpdateQuantity(quantity);
        UpdateTotalAmount();
        Touched();

        return true;
    }

    public bool RemoveItem(Guid saleItemId)
    {
        var item = _items.FirstOrDefault(x => x.Id == saleItemId);
        if (item is null)
        {
            return false;
        }

        _items.Remove(item);
        UpdateTotalAmount();
        Touched();

        return true;
    }
}