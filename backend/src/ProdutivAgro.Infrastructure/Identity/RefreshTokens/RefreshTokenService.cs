using System.Security.Cryptography;
using System.Text;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Infrastructure.Identity.RefreshTokens;

public sealed class RefreshTokenService(uint expirationDays) : IRefreshTokenService
{
    public string Generate() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    public string Hash(string refreshToken) => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));

    public DateTimeOffset GetExpirationDate(DateTimeOffset now) => now.AddDays(expirationDays);
}
