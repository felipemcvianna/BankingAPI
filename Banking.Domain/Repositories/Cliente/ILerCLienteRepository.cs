namespace Banking.Domain.Repositories.Cliente;

public interface ILerCLienteRepository
{
    public Task<bool> ExisteClienteComCpf(string cpf);
    public Task<bool> ExisteClienteComEmail(string email);

    public Task<Entities.Cliente?> GetClienteByEmail(string email);

    public Task<bool> ExisteClienteComIdentificador(Guid userIdentifier);
    public Task<Entities.Cliente?> GetClienteByNumeroConta(int numeroConta);
}