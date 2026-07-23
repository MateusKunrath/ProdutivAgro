using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ProdutivAgro.Application.Common.Security;
using ProdutivAgro.Domain.Identity.Enums;

namespace ProdutivAgro.Infrastructure.Identity;

public sealed class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public Guid UserId => GetGuidClaim(CustomClaims.UserId);

    public Guid OrganizationId => GetGuidClaim(CustomClaims.OrganizationId);

    public UserRole Role => Enum.Parse<UserRole>(GetClaim(CustomClaims.Role));

    private Guid GetGuidClaim(string claimType)
    {
        var value = GetClaim(claimType);

        return Guid.TryParse(value, out var guid)
            ? guid
            : Guid.Empty;
    }

    private string GetClaim(string claimType)
    {
        return User?.FindFirst(claimType)?.Value ?? string.Empty;
    }
}