using System.Net;

namespace ProdutivAgro.Exception.ExceptionsBase;

public class ForbiddenException(string message) : ProdutivAgroException(message)
{
    public override int StatusCode => (int)HttpStatusCode.Forbidden;

    public override List<string> GetErrors() => [Message];
}
