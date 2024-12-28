using Banking.Domain.Repositories.Conta;
using Banking.Exceptions.ExceptionBase;
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

        public async Task<List<Domain.Entities.Conta>> GetAllContas()
        {
            return _context.Contas.ToList();
        }
        public async Task<int> ObterUltimoNumeroConta()
        {
            //var contas = GetAllContas();
            //if (contas == null)
            //    return 0;

            var ultimaConta = await _context.Contas
                .OrderByDescending(x => x.NumeroConta)
                .FirstOrDefaultAsync();

            if (ultimaConta == null) return 0;

            return ultimaConta.NumeroConta;
        }

        public async Task DeletarConta(Domain.Entities.Conta conta)
        {
            _context.Contas.Remove(conta);
        }

        public async Task<Domain.Entities.Conta?> ObterConta(Guid userIdentifier, int numeroConta)
        {
            return await _context.Contas.Where(x => x.UserIdentifier.Equals(userIdentifier) 
            && x.NumeroConta == numeroConta).FirstOrDefaultAsync();

        }
    }
}