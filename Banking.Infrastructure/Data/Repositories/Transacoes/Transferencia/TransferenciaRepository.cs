using Banking.Domain.Repositories.Transacoes.Transferencia;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Data.Repositories.Transacoes.Transferencia;

public class TransferenciaRepository : IGravarTransferenciaRepository, ILerTransferenciaRepository
{
    private readonly BankingDbContext _context;

    public TransferenciaRepository(BankingDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.Transferencia transferencia) =>
        await _context.Transferencias.AddAsync(transferencia);

    public async Task<List<Domain.Entities.Transferencia>> GetAllTransferenciasAsync(string cpfCliente) =>
        await _context.Transferencias
            .AsNoTracking()
            .Where(x => x.CpfClienteOrigem == cpfCliente).ToListAsync();

    public async Task<List<Domain.Entities.Transferencia>> GetTransferenciaByDataAsync(DateTime dataTransferencia,
        string cpfCliente) =>
        await _context.Transferencias
            .AsNoTracking()
            .Where(x => x.DataTransacao.Date == dataTransferencia.Date &&
                        (x.CpfClienteOrigem == cpfCliente || x.CpfClienteDestino == cpfCliente)).ToListAsync();
}