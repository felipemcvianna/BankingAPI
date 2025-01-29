using Banking.Domain.Entities;

namespace Banking.Domain.Seguranca.Tokens
{
    public interface ILoggedCliente
    {
        public Task<Cliente> GetClienteByToken();
    }
}
