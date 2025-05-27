namespace Banking.Domain.Repositories.Transacoes.Transferencia;

public interface ILerTransferenciaRepository
{
    Task<List<Entities.Transferencia>> GetAllTransferenciasAsync(int idCliente);

    Task<List<Domain.Entities.Transferencia>>
        GetTransferenciaByDataAsync(DateTime dataTransferencia, int idCliente);

    Task<Entities.Transferencia?> GetTransferenciaByNumeroAsync(string numeroTransacao, int idCliente);
}