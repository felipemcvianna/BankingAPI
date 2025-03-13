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
        private readonly IGravarContaRepository _gravarContaRepository;

        public TransacaoService(IGravarContaRepository gravarContaRepository)
        {   
            _gravarContaRepository = gravarContaRepository;
        }

        public void ExecutarDeposito(Conta conta, double valorDeposito)
        {
            conta.AdicionarSaldo(valorDeposito);
        }

        public void ExecutarSaque(Conta conta, double valorSaque)
        {
            conta.RemoverSaldo(valorSaque);
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
    }
}