using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProdutivAgro.Communication.Responses;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ProdutivAgroException)
        {
            HandleProjectException(context);
            return;
        }

        ThrowUnknownError(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var produtivAgroException = (ProdutivAgroException)context.Exception;
        var errorMessages = new ResponseErrorJson(produtivAgroException.GetErrors());

        context.HttpContext.Response.StatusCode = produtivAgroException.StatusCode;
        context.Result = new ObjectResult(errorMessages);
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson("Ocorreu um erro inesperado");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}