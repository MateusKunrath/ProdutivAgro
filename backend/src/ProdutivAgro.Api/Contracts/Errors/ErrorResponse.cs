namespace ProdutivAgro.Api.Contracts.Errors;

/// <summary>
/// Standard error payload returned by the HTTP API.
/// </summary>
public sealed class ErrorResponse
{
    public ErrorResponse(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public ErrorResponse(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    public List<string> ErrorMessages { get; } = [];
}
