using Banking.Domain.Repositories.Transacoes.Saque;

namespace Banking.Infrastructure.Data.Repositories.Transacoes.Saque;

public class SaqueRepository : IGravarSaqueRepository
{
    private readonly BankingDbContext _context;

    public SaqueRepository(BankingDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.Saque saque)
    {
        await _context.Saques.AddAsync(saque);
    }
}