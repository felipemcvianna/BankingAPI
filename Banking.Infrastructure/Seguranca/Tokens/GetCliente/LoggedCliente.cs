using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Banking.Domain.Entities;
using Banking.Domain.Seguranca.Tokens;
using Banking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Seguranca.Tokens.GetCliente
{
    public class LoggedCliente : ILoggedCliente
    {
        private readonly ITokenRequest _tokenRequest;
        private readonly BankingDbContext _dbContext;

        public LoggedCliente(ITokenRequest tokenRequest, BankingDbContext dbContext)
        {
            _tokenRequest = tokenRequest;
            _dbContext = dbContext;
        }

        public async Task<Cliente?> GetClienteByToken()
        {
            var token = _tokenRequest.Value();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            var securityIdentifier = Guid.Parse(identifier);

            return await _dbContext.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserIdentifier == securityIdentifier);
        }
    }
}