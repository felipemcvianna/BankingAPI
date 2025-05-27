using System.Net;
using Banking.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Banking.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not ICustomHttpException customEx)
        {
            HandleUnknowException(context);
            return;
        }

        if (context.Exception is ICustomHttpException exception)
        {
            context.HttpContext.Response.StatusCode = exception.StatusCodes;
            context.Result = new ObjectResult(exception.ToResult());
        }
    }

    private void HandleUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(context.Exception.Message);
    }
}