using System.Transactions;
using Banking.Communication.Requests.Transacao;
using Banking.Communication.Response.Transacao;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Repositories.Conta;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Transacao.ExecutarTransacao
{
    public class ExecutarTransacaoUseCase : IExecutarTransacaoUseCase
    {
        private readonly ILoggedCliente _loggedCliente;
        private readonly ITransacaoService _transacaoService;

        public ExecutarTransacaoUseCase(ILoggedCliente loggedCliente, ITransacaoService transacaoService)
        {
            _loggedCliente = loggedCliente;
            _transacaoService = transacaoService;
        }

        public async Task<ResponseExecutarTransacaoJson> Execute(RequestExecutarTransacaoJson request)
        {


            var clienteAutenticado = await _loggedCliente.GetClienteByToken();
            var contaOrigem = await _transacaoService.ObterConta(clienteAutenticado!.UserIdentifier, clienteAutenticado.NumeroConta);


            var clienteDestino = await _transacaoService.ObterClienteByNumeroConta(request.numeroConta);
            var contaDestino = await _transacaoService.ObterConta(request.numeroConta, request.numeroBanco, request.numeroAgencia);

            await _transacaoService.ExecutarTransacao(contaOrigem, contaDestino, request.valorTransacao);



            return new ResponseExecutarTransacaoJson()
            {
                contaOrigem = new Domain.Entities.AuxiliarTransacao()
                {
                    numeroAgencia = contaOrigem.NumeroAgencia,
                    numeroBanco = contaOrigem.NumeroBanco,
                    numeroConta = contaOrigem.NumeroConta,
                },
                contaDestino = new Domain.Entities.AuxiliarTransacao()
                {
                    numeroAgencia = contaDestino.NumeroAgencia,
                    numeroBanco = contaDestino.NumeroBanco,
                    numeroConta = contaDestino.NumeroConta,
                },
                nomeClienteOrigem = clienteAutenticado.Nome,
                nomeClienteDestino = clienteDestino.Nome,
                valorTransacao = request.valorTransacao,
                CPFCliteOrigem = clienteAutenticado.CPF,
                CPFClienteDestino = clienteDestino.CPF
            };

        }
    }
}
