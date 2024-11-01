namespace Banking.Domain.Repositories.Cliente;

public interface ILerCLienteRepository
{
    public Task<bool> ExisteClienteComCpf(string cpf);
}       