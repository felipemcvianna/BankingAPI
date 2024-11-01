using Banking.Domain.Repositories;

namespace Banking.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BankingDbContext _context;

    public UnitOfWork(BankingDbContext context)
    {
        _context = context;
    }

    public async Task Commit() => await _context.SaveChangesAsync();
}