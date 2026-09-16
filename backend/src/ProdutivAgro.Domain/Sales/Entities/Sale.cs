using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Products.Entities;
using ProdutivAgro.Domain.Sales.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Sales.Entities;

public class Sale : AggregateRoot
{
    private readonly List<SaleItem> _items = [];
    private readonly List<SaleStatusHistory> _statusHistory = [];

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

    public Guid CreatedByUserId { get; }
    public Guid? UpdatedByUserId { get; private set; }
    public User CreatedByUser { get; private set; } = null!;
    public User? UpdatedByUser { get; private set; }

    public SaleStatus Status { get; private set; }

    public decimal TotalAmount { get; private set; }

    public DateTimeOffset SoldAt { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    public IReadOnlyCollection<SaleItem> Items => _items;
    public IReadOnlyCollection<SaleStatusHistory> SalesStatusHistory => _statusHistory;

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
        Touched(CreatedByUserId);

        return item;
    }

    private void UpdateTotalAmount()
    {
        TotalAmount = _items.Sum(x => x.TotalAmount);
    }

    private void Touched(Guid updatedByUserId)
    {
        UpdatedAt = DateTimeOffset.UtcNow;
        UpdatedByUserId = updatedByUserId;
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
        Touched(CreatedByUserId);

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
        Touched(CreatedByUserId);

        return true;
    }

    public bool TryComplete(Guid userId, out SaleStatusHistory? history)
    {
        history = null;

        if (_items.Count == 0 || Status != SaleStatus.Draft)
        {
            return false;
        }

        history = new SaleStatusHistory(Id, Status, SaleStatus.Completed, userId);

        _statusHistory.Add(history);
        Status = SaleStatus.Completed;
        Touched(userId);

        return true;
    }

    public bool TryCancel(Guid userId, string reason, out SaleStatusHistory? history)
    {
        history = null;

        if (Status is not (SaleStatus.Draft or SaleStatus.Completed))
        {
            return false;
        }

        history = new SaleStatusHistory(Id, Status, SaleStatus.Cancelled, userId, reason);

        _statusHistory.Add(history);
        Status = SaleStatus.Cancelled;
        Touched(userId);

        return true;
    }

    public bool TryUndoCancellation(Guid userId, string reason, out SaleStatusHistory? history)
    {
        history = null;

        var lastHistory = GetLastStatusHistory();

        if (
            Status != SaleStatus.Cancelled ||
            lastHistory is null ||
            lastHistory.CurrentStatus != SaleStatus.Cancelled
        )
        {
            return false;
        }

        var restoredStatus = lastHistory.PreviousStatus;

        history = new SaleStatusHistory(Id, Status, restoredStatus, userId, reason);

        _statusHistory.Add(history);
        Status = restoredStatus;
        Touched(userId);

        return true;
    }

    public bool TryReopen(Guid userId, string reason, out SaleStatusHistory? history)
    {
        history = null;

        if (Status != SaleStatus.Completed)
        {
            return false;
        }

        history = new SaleStatusHistory(Id, Status, SaleStatus.Draft, userId, reason);

        _statusHistory.Add(history);
        Status = SaleStatus.Draft;
        Touched(userId);

        return true;
    }

    private SaleStatusHistory? GetLastStatusHistory()
    {
        return _statusHistory.OrderByDescending(x => x.ChangedAt).FirstOrDefault();
    }
}