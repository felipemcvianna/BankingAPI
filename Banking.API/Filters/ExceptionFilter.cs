using System.Net;
using Banking.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Banking.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not BankingExceptions) HandleUnknowException(context);
        HandleRegisterException(context);
        HandleDeleteException(context);
    }

    private void HandleRegisterException(ExceptionContext context)
    {
        if (context.Exception is ErrorsOnValidateExceptions exception)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(exception!.Erros);
        }
    }

    private void HandleDeleteException(ExceptionContext context)
    {
        if (context.Exception is BusinessException exception)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(exception!.Erros);
        }
    }

    private void HandleUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
}