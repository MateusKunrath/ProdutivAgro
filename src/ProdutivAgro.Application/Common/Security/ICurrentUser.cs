using ProdutivAgro.Domain.Identity.Enums;

namespace ProdutivAgro.Application.Common.Security;

public interface ICurrentUser
{
    Guid UserId { get; }

    Guid OrganizationId { get; }

    UserRole Role { get; }

    bool IsAuthenticated { get; }
}