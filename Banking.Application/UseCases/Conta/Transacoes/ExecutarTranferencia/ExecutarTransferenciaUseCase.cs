using Banking.Application.Services.Transacao;
using Banking.Application.UseCases.Transacao.ExecutarTranferencia;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.ExecutarTranferencia
{
    public class ExecutarTransferenciaUseCase : IExecutarTransferenciaUseCase
    {
        private readonly ILoggedCliente _loggedCliente;
        private readonly ITransacaoService _transacaoService;
        public ExecutarTransferenciaUseCase(ILoggedCliente loggedCliente, ITransacaoService transacaoService)
        {
            _loggedCliente = loggedCliente;
            _transacaoService = transacaoService;
        }
        public async Task<ResponseExecutarTransferenciaJson> Execute(RequestExecutarTransacaoJson request)
        {
            if (!double.TryParse(request.valorTransacao, out double valor) || valor <= 0)
                throw new BusinessException("O valor da transação é inválido.");

            var clienteAutenticado = await _loggedCliente.GetClienteByToken();
            var contaOrigem = await _transacaoService.ObterConta(clienteAutenticado!.UserIdentifier, clienteAutenticado.NumeroConta);


            var clienteDestino = await _transacaoService.ObterClienteByNumeroConta(request.numeroConta);
            var contaDestino = await _transacaoService.ObterConta(request.numeroConta, request.numeroBanco, request.numeroAgencia);

            await _transacaoService.ExecutarTransferencia(contaOrigem, contaDestino, valor);

            return new ResponseExecutarTransferenciaJson()
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
                valorTransacao = valor,
                CPFClienteOrigem = clienteAutenticado.CPF,
                CPFClienteDestino = clienteDestino.CPF,
                // numeroTransacao = SegurancaTransacao.GerarNumeroTransacao()
            };
        }
    }
}
