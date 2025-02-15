namespace Banking.Domain.Repositories.Transacoes.Saque;

public interface ILerSaqueRepository
{
    public Task<List<Entities.Saque>> LerTodosSaquesClienteAutenticado(int numeroConta, int numeroBanco,
        int numeroAgencia);
}