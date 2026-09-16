using System.Net;

namespace ProdutivAgro.Exception.ExceptionsBase;

public class InvalidAuthenticationException()
    : ProdutivAgroException(ResourceErrorMessages.IDENTIFIER_OR_PASSWORD_INVALID)
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}