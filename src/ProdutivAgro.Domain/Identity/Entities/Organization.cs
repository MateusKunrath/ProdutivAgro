using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Identity.Entities;

public class Organization : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public bool Active { get; private set; }
}