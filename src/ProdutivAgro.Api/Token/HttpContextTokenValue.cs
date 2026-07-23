using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Api.Token;

public class HttpContextTokenValue(IHttpContextAccessor httpContextAccessor) : ITokenProvider
{
    public string TokenOnRequest()
    {
        var authorization = httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authorization["Bearer ".Length..].Trim();
    }
}