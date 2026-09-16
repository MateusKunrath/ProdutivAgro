using ProdutivAgro.Domain.Identity.Enums;

namespace ProdutivAgro.Domain.Identity.Extensions;

public static class UserRoleExtensions
{
    public static string RoleToString(this UserRole userRole)
    {
        return userRole switch
        {
            UserRole.Administrator => nameof(UserRole.Administrator),
            UserRole.Manager => nameof(UserRole.Manager),
            UserRole.Employee => nameof(UserRole.Employee),
            _ => string.Empty,
        };
    }
}