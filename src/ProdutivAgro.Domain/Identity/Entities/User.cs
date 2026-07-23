using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Domain.Shared;

namespace ProdutivAgro.Domain.Identity.Entities;

public class User : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public Guid OrganizationId { get; private set; }
    public UserRole Role { get; private set; } = UserRole.TeamMember;
    public UserStatus Active { get; private set; } = UserStatus.Active;
}