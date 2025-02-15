namespace Banking.Domain.Repositories.Transacoes.Saque;

public interface IGravarSaqueRepository
{
    public Task Add(Entities.Saque saque);
}