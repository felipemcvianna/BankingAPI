namespace Banking.Application.UseCases.Transacao.ExecutarTransacao
{
    public interface ITransacaoService
    {
        public Task ExecutarTransacao(Domain.Entities.Conta contaOrigem, Domain.Entities.Conta contaDestino, double valorTransacao);
        public Task<Domain.Entities.Conta> ObterConta(int numeroConta, int numeroBanco, int numeroAgencia);

        public Task<Domain.Entities.Conta> ObterConta(Guid userIdentifier,int numeroConta);

        public Task<Domain.Entities.Cliente> ObterClienteByNumeroConta(int numeroConta);
    }
}
