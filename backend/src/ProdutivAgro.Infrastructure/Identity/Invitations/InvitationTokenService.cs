using System.Security.Cryptography;
using System.Text;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Infrastructure.Identity.Invitations;

public sealed class InvitationTokenService(uint expirationDays) : IInvitationTokenService
{
    public string Generate()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public string Hash(string token)
    {
        return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }

    public DateTimeOffset GetExpirationDate(DateTimeOffset now)
    {
        return now.AddDays(expirationDays);
    }
}