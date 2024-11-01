namespace Banking.Domain.Repositories.Cliente;

public interface IGravarClienteRepository
{
    public Task Add(Entities.Cliente cliente);
}