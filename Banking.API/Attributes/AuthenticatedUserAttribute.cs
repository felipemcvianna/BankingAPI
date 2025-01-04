using Banking.API.Filters;
using Banking.Domain.Repositories.Cliente;
using Banking.Exceptions.ExceptionBase;
using Banking.Infrastructure.Seguranca.Tokens.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Banking.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {
        }
    }
}
