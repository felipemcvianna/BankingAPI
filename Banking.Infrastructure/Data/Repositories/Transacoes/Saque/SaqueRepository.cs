using Banking.Domain.Repositories.Transacoes.Saque;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Data.Repositories.Transacoes.Saque;

public class SaqueRepository : IGravarSaqueRepository, ILerSaqueRepository
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

    public async Task<List<Domain.Entities.Saque>> LerTodosSaquesClienteAutenticado(int numeroConta, int numeroBanco,
        int numeroAgencia) => await _context.Saques.Where(x =>
        x.ContaSaque.NumeroConta == numeroConta && x.ContaSaque.NumeroConta == numeroBanco &&
        x.ContaSaque.NumeroConta == numeroAgencia).ToListAsync();
}