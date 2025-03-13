namespace Banking.Application.Services.Transacao
{
    public interface ITransacaoService
    {
        public Task ExecutarTransferencia(Domain.Entities.Conta contaOrigem, Domain.Entities.Conta contaDestino,
            double valorTransacao);

        public void ExecutarDeposito(Domain.Entities.Conta conta, double valorDeposito);
        public void ExecutarSaque(Domain.Entities.Conta conta, double valorSaque);
    }
}