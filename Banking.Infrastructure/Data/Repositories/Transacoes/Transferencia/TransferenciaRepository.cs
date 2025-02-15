using Banking.Domain.Repositories.Transacoes.Transferencia;

namespace Banking.Infrastructure.Data.Repositories.Transacoes.Transferencia;

public class TransferenciaRepository : IGravarTransferenciaRepository
{
    private readonly BankingDbContext _context;

    public TransferenciaRepository(BankingDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.Transferencia transferencia) =>
        await _context.Transferencias.AddAsync(transferencia);
}