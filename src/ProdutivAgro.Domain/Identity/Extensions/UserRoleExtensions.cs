using ProdutivAgro.Domain.Identity.Enums;

namespace ProdutivAgro.Domain.Identity.Extensions;

public static class UserRoleExtensions
{
    public static string RoleToString(this UserRole userRole)
    {
        return userRole switch
        {
            UserRole.Administrator => nameof(UserRole.Administrator),
            UserRole.TeamMember => nameof(UserRole.TeamMember),
            _ => string.Empty,
        };
    }
}