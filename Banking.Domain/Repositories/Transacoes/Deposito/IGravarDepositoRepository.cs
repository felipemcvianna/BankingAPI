namespace Banking.Domain.Repositories.Transacoes.Deposito
{
    public interface IGravarDepositoRepository
    {
        public Task Add(Entities.Deposito deposito);
    }
}
