using ProdutivAgro.Application.Sales.Shared;
using ProdutivAgro.Application.Shared;
using System.Text.Json.Serialization;

namespace ProdutivAgro.Application.Sales.Queries.GetSaleById;

public sealed class GetSaleByIdResult
{
    public Guid Id { get; init; }
    public OrganizationIdResult Organization { get; init; } = new();
    public SaleUserResult CreatedUser { get; init; } = new();
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SaleUserResult? UpdatedUser { get; init; }
    public List<GetSaleItemResult> Items { get; init; } = [];
    public string Status { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
    public DateTimeOffset SoldAt { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
