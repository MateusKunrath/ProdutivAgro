namespace ProdutivAgro.Api.Authentication;

public sealed class AuthenticationCookieService(IConfiguration configuration)
{
    public const string AccessTokenCookieName = "access_token";
    public const string RefreshTokenCookieName = "refresh_token";

    private readonly TimeSpan _accessTokenLifetime = TimeSpan.FromMinutes(
        configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes"));

    private readonly TimeSpan _refreshTokenLifetime = TimeSpan.FromDays(
        configuration.GetValue<uint>("Settings:RefreshToken:ExpiresDays", 30));

    public void SetAuthenticationCookies(HttpResponse response, string accessToken, string refreshToken)
    {
        response.Cookies.Append(AccessTokenCookieName, accessToken, CreateAccessTokenOptions());
        response.Cookies.Append(RefreshTokenCookieName, refreshToken, CreateRefreshTokenOptions());
    }

    public string GetRefreshToken(HttpRequest request) =>
        request.Cookies.TryGetValue(RefreshTokenCookieName, out var refreshToken)
            ? refreshToken
            : string.Empty;

    public void ClearAuthenticationCookies(HttpResponse response)
    {
        response.Cookies.Delete(AccessTokenCookieName, CreateAccessTokenOptions());
        response.Cookies.Delete(RefreshTokenCookieName, CreateRefreshTokenOptions());
    }

    private CookieOptions CreateAccessTokenOptions() => CreateCookieOptions(_accessTokenLifetime, "/");

    private CookieOptions CreateRefreshTokenOptions() => CreateCookieOptions(_refreshTokenLifetime, "/api/Auth");

    private static CookieOptions CreateCookieOptions(TimeSpan lifetime, string path) => new()
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        Path = path,
        MaxAge = lifetime,
        IsEssential = true,
    };
}
