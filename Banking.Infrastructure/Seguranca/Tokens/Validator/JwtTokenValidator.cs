
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions.ExceptionBase;
using Banking.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Banking.Infrastructure.Seguranca.Tokens.Validator
{
    public class JwtTokenValidator : TokenSigningKey, IJwtTokenValidator
    {
        private readonly string _signingKey;

        public JwtTokenValidator(string signingKey)
        {
            _signingKey = signingKey;
        }

        public Guid ValidateAndGetUserIdentifier(string token)
        {

            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = new TimeSpan(0),
                IssuerSigningKey = SecurityKey(_signingKey)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            var userIdentifier = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

            if (userIdentifier == null)
                throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);


            return Guid.Parse(userIdentifier!.Value);
        }
    }
}
