using System.Net;

namespace ProdutivAgro.Exception.ExceptionsBase;

public class NotFoundException(string message) : ProdutivAgroException(message)
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}