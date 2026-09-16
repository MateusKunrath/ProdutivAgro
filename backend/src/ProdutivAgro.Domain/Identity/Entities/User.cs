using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Identity.Entities;

public class User : AggregateRoot
{
    public User(string name, string email, Guid organizationId, UserRole role)
    {
        Name = name;
        Email = email;
        OrganizationId = organizationId;
        Role = role;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; } = string.Empty;
    public Guid OrganizationId { get; private set; }
    public UserRole Role { get; private set; }
    public UserStatus Active { get; private set; } = UserStatus.Active;

    public void SetPasswordHash(string passwordHash)
    {
        Password = passwordHash;
    }
}