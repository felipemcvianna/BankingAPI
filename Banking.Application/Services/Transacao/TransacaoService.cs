using Banking.Communication.Requests.Conta.Transacao;
using Banking.Domain.Entities;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Repositories.Conta;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.Services.Transacao
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ILerContaRepository _lerContaRepository;
        private readonly IGravarContaRepository _gravarContaRepository;
        private readonly ILerCLienteRepository _clienteRepository;

        public TransacaoService(ILerContaRepository lerContaRepository,
            IGravarContaRepository gravarContaRepository, ILerCLienteRepository clienteRepository)
        {
            _lerContaRepository = lerContaRepository;
            _gravarContaRepository = gravarContaRepository;
            _clienteRepository = clienteRepository;
        }

        public void ExecutarDeposito(Conta conta, double valorDeposito)
        {
            conta.AdicionarSaldo(valorDeposito);
        }

        public async Task<Conta> ExecutarSaque(RequestSaqueJson request)
        {
            var conta = await ObterConta(request.numeroConta, request.numeroBanco,
                request.numeroAgencia);

            conta.RemoverSaldo(double.Parse(request.ValorTransacao));

            return conta;
        }

        public async Task ExecutarTransferencia(Conta contaOrigem, Conta contaDestino,
            double valor)
        {
            await using var transaction = await _gravarContaRepository.ComecarTransacaoAsync();

            try
            {
                if (contaOrigem == null || contaDestino == null)
                    throw new BusinessException(ResourceMessagesExceptions.CONTA_NAO_ENCONTRADA);

                if (contaOrigem.NumeroConta == contaDestino.NumeroConta)
                    throw new BusinessException("NÃO É POSSÍVEL REALIZAR UMA TRANSFERÊNCIA PARA A PRÓPRIA CONTA");


                contaOrigem.RemoverSaldo(valor);
                contaDestino.AdicionarSaldo(valor);

                _gravarContaRepository.Atualizar(contaOrigem);
                _gravarContaRepository.Atualizar(contaDestino);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Cliente> ObterClienteByNumeroConta(int numeroConta)
        {
            var cliente = await _clienteRepository.GetClienteByNumeroConta(numeroConta);

            if (cliente == null)
                throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

            return cliente;
        }

        public async Task<Conta> ObterConta(int numeroConta, int numeroBanco, int numeroAgencia)
        {
            var conta = await _lerContaRepository.ObterConta(numeroConta, numeroBanco, numeroAgencia);

            if (conta == null)
                throw new BusinessException(ResourceMessagesExceptions.CONTA_NAO_ENCONTRADA);

            return conta;
        }

        public async Task<Conta> ObterConta(Guid userIdentifier, int numeroConta)
        {
            var conta = await _lerContaRepository.ObterConta(userIdentifier, numeroConta);

            if (conta == null)
                throw new BusinessException(ResourceMessagesExceptions.CONTA_NAO_ENCONTRADA);

            return conta;
        }
    }
}