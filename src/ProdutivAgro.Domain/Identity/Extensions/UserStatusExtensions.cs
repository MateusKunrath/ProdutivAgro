using ProdutivAgro.Domain.Identity.Enums;

namespace ProdutivAgro.Domain.Identity.Extensions;

public static class UserStatusExtensions
{
    public static string StatusToString(this UserStatus userStatus)
    {
        return userStatus switch
        {
            UserStatus.Active => nameof(UserStatus.Active),
            UserStatus.Inactive => nameof(UserStatus.Inactive),
            _ => string.Empty,
        };
    }
}