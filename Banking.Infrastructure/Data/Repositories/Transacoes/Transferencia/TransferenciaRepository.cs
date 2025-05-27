using Banking.Domain.Repositories.Transacoes.Transferencia;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Data.Repositories.Transacoes.Transferencia;

public class TransferenciaRepository(BankingDbContext context)
    : IGravarTransferenciaRepository, ILerTransferenciaRepository
{
    public async Task Add(Domain.Entities.Transferencia transferencia) =>
        await context.Transferencias.AddAsync(transferencia);

    public async Task<List<Domain.Entities.Transferencia>> GetAllTransferenciasAsync(int idCliente) =>
        await context.Transferencias
            .Include(c => c.ClienteOrigem.Conta)
            .Include(c => c.ClienteDestino.Conta)
            .Where(x => x.IdClienteOrigem == idCliente || x.IdClienteDestino == idCliente).ToListAsync();

    public async Task<List<Domain.Entities.Transferencia>> GetTransferenciaByDataAsync(DateTime dataTransferencia,
        int idCliente) =>
        await context.Transferencias
            .Include(c => c.ClienteOrigem.Conta)
            .Include(c => c.ClienteDestino.Conta)
            .Where(d => d.DataTransacao.Date == dataTransferencia.Date &&
                        (d.ClienteOrigem.Id == idCliente || d.ClienteDestino.Id == idCliente))
            .ToListAsync();

    public async Task<Domain.Entities.Transferencia?> GetTransferenciaByNumeroAsync(string numeroTransacao,
        int idCliente) =>
        await context.Transferencias
            .Include(c => c.ClienteOrigem.Conta)
            .Include(c => c.ClienteDestino.Conta)
            .FirstOrDefaultAsync(x =>
                x.NumeroTransacao == numeroTransacao && x.IdClienteOrigem == idCliente);
}