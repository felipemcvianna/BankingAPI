using System.Net;
using Banking.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Banking.API.Filter;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BankingExceptions)
        {
            
        }
        HandleRegisterException(context);
        
    }

    private void HandleRegisterException(ExceptionContext context)
    {
        if (context.Exception is ErrorsOnValidateExceptions exception)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(exception!.Erros);
        }
        
    }
    

    private void HandleUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult("Não sei o que está passando");
    }
}