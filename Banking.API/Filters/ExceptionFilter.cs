using System.Net;
using Banking.Exceptions.ExceptionBase;
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

        switch (context.Exception)
        {
            case ErrorsOnValidateExceptions:
                HandleRegisterException(context);
                return;
            case BusinessException:
                HandleDeleteException(context);
                return;
            case DepositoException:
                HandleDepositoExceptions(context);
                return;
        }
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

    private void HandleDepositoExceptions(ExceptionContext context)
    {
        if (context.Exception is not DepositoException exception) return;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(exception.Errors);
    }

    private void HandleUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult("Server error");
    }
}