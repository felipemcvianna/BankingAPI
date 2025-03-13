namespace Banking.Domain.Repositories.Transacoes.Deposito;

public interface ILerDepositosRepository
{
    public Task<List<Entities.Deposito>> GetAllDepositos(string cpfCliente);
    public Task<List<Domain.Entities.Deposito>> GetDepositosByPeriodo(DateTime dataInicial, DateTime dataFinal);
}