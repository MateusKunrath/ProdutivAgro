namespace ProdutivAgro.Application.Abstractions.Authentication;

public interface IRefreshTokenService
{
    string Generate();
    string Hash(string refreshToken);
    DateTimeOffset GetExpirationDate(DateTimeOffset now);
}
