using System.Net;
using Banking.Exceptions.ExceptionBase;
using Banking.Exceptions.ExceptionBase.Deposito;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Banking.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not BankingExceptions)
        {
            HandleUnknowException(context);
            return;
        }

        HandleRegisterException(context);
        HandleDeleteException(context);
        HandleDataDepositoExceptions(context);
    }

    private void HandleRegisterException(ExceptionContext context)
    {
        if (context.Exception is not ErrorsOnValidateExceptions exception) return;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(exception.Errors);
    }

    private void HandleDeleteException(ExceptionContext context)
    {
        if (context.Exception is not BusinessException exception) return;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(exception.Errors);
    }

    private void HandleDataDepositoExceptions(ExceptionContext context)
    {
        if (context.Exception is not DataDepositoException exception) return;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(exception.Errors);
    }

    private void HandleUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult("Server error");
    }
}