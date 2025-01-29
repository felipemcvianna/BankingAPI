using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Banking.Infrastructure.Seguranca.Tokens
{
    public abstract class TokenSigningKey
    {
        protected SymmetricSecurityKey SecurityKey(string signingKey)
        {
            var bytes = Encoding.UTF8.GetBytes(signingKey);

            return new SymmetricSecurityKey(bytes);
        }
    }
}
