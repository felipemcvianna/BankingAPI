using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Cliente.Ler.ByToken
{
    public class GetClienteByTokenUseCase : IGetClienteByTokenUseCase
    {
        private readonly ILoggedCliente _loggedCliente;

        public GetClienteByTokenUseCase(ILoggedCliente loggedCliente)
        {
            _loggedCliente = loggedCliente;
        }

        public async Task<ResponseGetClienteByToken> Execute()
        {
            var cliente = await _loggedCliente.GetClienteByToken();

            if (cliente == null)
                throw new BusinessException("");

            return new ResponseGetClienteByToken()
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
            };
        }
    }
}
