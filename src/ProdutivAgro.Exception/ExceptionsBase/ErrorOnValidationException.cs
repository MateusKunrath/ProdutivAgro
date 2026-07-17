using System.Net;

namespace ProdutivAgro.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorMessages) : ProdutivAgroException(string.Empty)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return errorMessages;
    }
}