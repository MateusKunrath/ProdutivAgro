using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Identity.Entities;

public class Organization : AggregateRoot
{
    public Organization(string name)
    {
        Name = name;
    }

    public Organization() { }

    public string Name { get; private set; } = string.Empty;
    public bool Active { get; private set; } = true;
    public Guid? ResponsibleUserId { get; private set; }

    public void SetResponsibleUser(Guid userId)
    {
        ResponsibleUserId = userId;
    }
}
