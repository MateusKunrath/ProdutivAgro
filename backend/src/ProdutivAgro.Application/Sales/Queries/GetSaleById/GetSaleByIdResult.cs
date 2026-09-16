using System.Text.Json.Serialization;
using ProdutivAgro.Application.Shared;

namespace ProdutivAgro.Application.Sales.Queries.GetSaleById;

public sealed class GetSaleByIdResult
{
    public Guid Id { get; init; }
    public OrganizationIdResult Organization { get; init; } = new();
    public UserResult CreatedUser { get; init; } = new();

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserResult? UpdatedUser { get; init; }

    public List<GetSaleItemResult> Items { get; init; } = [];
    public string Status { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
    public DateTimeOffset SoldAt { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}