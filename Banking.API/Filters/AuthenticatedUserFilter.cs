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
        private readonly ITokenRequest _tokenOnRequest;

        public AuthenticatedUserFilter(
            IJwtTokenValidator jwtTokenValidator,
            ILerCLienteRepository lerCLienteRepository,
            ITokenRequest tokenOnRequest)
        {
            _jwtTokenValidator = jwtTokenValidator;
            _lerCLienteRepository = lerCLienteRepository;
            _tokenOnRequest = tokenOnRequest;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var Token = _tokenOnRequest.Value();


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
            catch
            {
                context.Result =
                    new UnauthorizedObjectResult(
                        new ResponseTokenErrorJson(ResourceMessagesExceptions.USUARIO_SEM_PERMISSAO));
            }
        }
    }
}