namespace ProdutivAgro.Exception.ExceptionsBase;

public abstract class ProdutivAgroException(string message) : SystemException(message)
{
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}