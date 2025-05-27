namespace Banking.Domain.Repositories.Transacoes.Deposito;

public interface ILerDepositosRepository
{
    public Task<List<Entities.Deposito>> GetAllDepositos(int idCliente);
    public Task<List<Entities.Deposito>> GetDepositosByPeriodo(DateTime dataInicial, DateTime dataFinal, int idCliente);
    public Task<List<Entities.Deposito>> ObterDepositoByData(DateTime dataDeposito, int idCliente);
    public Task<Entities.Deposito?> ObterDepositoPorNumero(string numero, int idCliente);
}