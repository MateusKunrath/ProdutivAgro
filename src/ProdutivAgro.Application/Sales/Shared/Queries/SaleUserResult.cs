namespace ProdutivAgro.Application.Sales.Shared.Queries;

public class SaleUserResult
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}