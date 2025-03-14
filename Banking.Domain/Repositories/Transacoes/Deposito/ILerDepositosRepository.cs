namespace Banking.Domain.Repositories.Transacoes.Deposito;

public interface ILerDepositosRepository
{
    public Task<List<Entities.Deposito>> GetAllDepositos(string cpfCliente);
    public Task<List<Entities.Deposito>> GetDepositosByPeriodo(DateTime dataInicial, DateTime dataFinal);
    public Task<List<Entities.Deposito>> ObterDepositoByData(DateTime dataDeposito);
}