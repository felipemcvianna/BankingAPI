using System.Security.Cryptography.X509Certificates;
using Banking.Domain.Repositories.Conta;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Data.Repositories.Conta
{
    public class ContaRepository : ILerContaRepository, IGravarContaRepository
    {
        private readonly BankingDbContext _context;

        public ContaRepository(BankingDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Domain.Entities.Conta conta)
            => await _context.Contas.AddAsync(conta);

        public async Task<bool> ExisteConta(int numeroConta)
            => await _context.Contas.AnyAsync(x => x.NumeroConta == numeroConta);

        public async Task<int> ObterUltimoNumeroConta()
        {
            var ultimaConta = await _context.Contas
                .OrderByDescending(x => x.NumeroConta)
                .FirstOrDefaultAsync();

            if (ultimaConta == null) return 0;

            return ultimaConta.NumeroConta;
        }
    }
}