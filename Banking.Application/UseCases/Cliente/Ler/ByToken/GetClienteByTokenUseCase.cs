using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories.Conta;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Cliente.Ler.ByToken
{
    public class GetClienteByTokenUseCase : IGetClienteByTokenUseCase
    {
        private readonly ILoggedCliente _loggedCliente;
        private readonly ILerContaRepository _contaRepository;

        public GetClienteByTokenUseCase(ILoggedCliente loggedCliente,
            ILerContaRepository contaRepository)
        {
            _loggedCliente = loggedCliente;
            _contaRepository = contaRepository;
        }

        public async Task<ResponseGetClienteByToken> Execute()
        {
            var cliente = await _loggedCliente.GetClienteByToken();

            if (cliente == null)
                throw new BusinessException(ResourceMessagesExceptions.USUARIO_NAO_ENCONTRADO);

            var conta = await _contaRepository.ObterConta(cliente.UserIdentifier, cliente.NumeroConta);

            if (conta == null)
                throw new BusinessException(ResourceMessagesExceptions.CONTA_NAO_ENCONTRADA);

            return new ResponseGetClienteByToken()
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                contaCliente = new Communication.Response.Conta.ResponseGetContaByTokenJson()
                {
                    numeroConta = conta.NumeroConta,
                    numeroBanco =  conta.NumeroBanco,
                    numeroAgencia = conta.NumeroAgencia,
                    dataCriacao = conta.DataCriacao,
                }
            };
        }

    }
}
