using Banking.Communication.Requests.Conta.Transacao;

namespace Banking.Application.Services.Transacao
{
    public interface ITransacaoService
    {
        public Task ExecutarTransferencia(Domain.Entities.Conta contaOrigem, Domain.Entities.Conta contaDestino,
            double valorTransacao);

        public Task<Domain.Entities.Conta> ObterConta(int numeroConta, int numeroBanco, int numeroAgencia);

        public Task<Domain.Entities.Conta> ObterConta(Guid userIdentifier, int numeroConta);

        public Task<Domain.Entities.Cliente> ObterClienteByNumeroConta(int numeroConta);

        public void ExecutarDeposito(Domain.Entities.Conta conta, double valorDeposito);
        public Task<Domain.Entities.Conta> ExecutarSaque(RequestSaqueJson request);
    }
}