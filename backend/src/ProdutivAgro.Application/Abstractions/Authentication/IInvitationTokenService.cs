namespace ProdutivAgro.Application.Abstractions.Authentication;

public interface IInvitationTokenService
{
    string Generate();
    string Hash(string token);
    DateTimeOffset GetExpirationDate(DateTimeOffset now);
}