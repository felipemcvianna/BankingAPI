namespace Banking.Domain.Repositories.Cliente;

public interface ILerCLienteRepository
{
    public Task<bool> ExisteClienteComCpf(string cpf);
    public Task<bool> ExisteClienteComEmail(int? id, string email);

    public Task<Entities.Cliente> GetClienteByEmail(string email);
}       