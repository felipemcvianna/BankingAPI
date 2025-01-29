using System.Net;
using Banking.Communication.Token;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Banking.API.Filters
{
    public class AuthenticatedUserFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IJwtTokenValidator _jwtTokenValidator;
        private readonly ILerCLienteRepository _lerCLienteRepository;

        public AuthenticatedUserFilter(
            IJwtTokenValidator jwtTokenValidator,
            ILerCLienteRepository lerCLienteRepository)
        {
            _jwtTokenValidator = jwtTokenValidator;
            _lerCLienteRepository = lerCLienteRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var Token = TokenOnRequest(context);


                var userIdentifier = _jwtTokenValidator.ValidateAndGetUserIdentifier(Token);

                var exist = await _lerCLienteRepository.ExisteClienteComIdentificador(userIdentifier);

                if (!exist)
                    throw new BankingExceptions(ResourceMessagesExceptions.USUARIO_SEM_PERMISSAO);
            }
            catch (BankingExceptions ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseTokenErrorJson(ex.Message));
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseTokenErrorJson("TokenIsExpires")
                {
                    TokenExpires = true
                });
            }
            catch (ArgumentNullException ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseTokenErrorJson(ex.Message));
            }            
        }
        public string TokenOnRequest(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                throw new ArgumentNullException("",ResourceMessagesExceptions.SEM_TOKEN);

            var token = authorizationHeader.ToString();

            if (!token.StartsWith("Bearer "))
                throw new ArgumentNullException("",ResourceMessagesExceptions.TOKEN_INVALIDO);

            return token["Bearer ".Length..].Trim();
        }

    }
}
