namespace Banking.Domain.Repositories.Transacoes.Transferencia;

public interface IGravarTransferenciaRepository
{
    public Task Add(Entities.Transferencia transferencia);
}