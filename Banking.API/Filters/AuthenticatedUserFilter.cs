﻿using Banking.Communication.Response;
using Banking.Communication.Token;
using Banking.Domain.Repositories.Cliente;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;
using Banking.Infrastructure.Seguranca.Tokens.Validator;
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
                    throw new BusinessException(ResourceMessagesExceptions.USUARIO_SEM_PERMISSAO);
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
            catch
            {
                context.Result = new UnauthorizedObjectResult(new BusinessException(ResourceMessagesExceptions.USUARIO_SEM_PERMISSAO));
            }

        }

        public string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.ToString();

            if (string.IsNullOrWhiteSpace(authentication))
                throw new BusinessException(ResourceMessagesExceptions.USUARIO_SEM_PERMISSAO);

            return authentication!["Bearer ".Length..].Trim();
        }
    }
}
