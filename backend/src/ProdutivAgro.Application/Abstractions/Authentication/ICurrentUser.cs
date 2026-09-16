using ProdutivAgro.Domain.Identity.Enums;

namespace ProdutivAgro.Application.Abstractions.Authentication;

public interface ICurrentUser
{
    Guid UserId { get; }

    Guid OrganizationId { get; }

    UserRole Role { get; }

    bool IsAuthenticated { get; }
}