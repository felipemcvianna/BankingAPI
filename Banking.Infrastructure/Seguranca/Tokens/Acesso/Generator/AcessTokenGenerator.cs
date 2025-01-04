using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Banking.Domain.Seguranca.Tokens.Generate;
using Microsoft.IdentityModel.Tokens;

namespace Banking.Infrastructure.Seguranca.Tokens.Acesso.Generator
{
    public class AcessTokenGenerator : TokenSigningKey, IAcessTokenGenerator
    {
        private readonly string _signingKey;
        private readonly uint _expirationTimeMinutes;

        public AcessTokenGenerator(string signingKey, uint expirationTimeMinutes)
        {
            _signingKey = signingKey;
            _expirationTimeMinutes = expirationTimeMinutes;
        }

        public string GenerateToken(Guid UserIdentifier)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, UserIdentifier.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
       
    }
}
