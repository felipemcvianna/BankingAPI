namespace Banking.Domain.Repositories.Transacoes.Transferencia;

public interface ILerTransferenciaRepository
{
    Task<List<Entities.Transferencia>> GetAllTransferenciasAsync(string cpfCliente);
    Task<List<Domain.Entities.Transferencia>> GetTransferenciaByDataAsync(DateTime dataTransferencia, string cpfCliente);
}