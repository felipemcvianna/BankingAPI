using System.Data;
using Banking.Domain.Repositories.Conta;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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

        public List<Domain.Entities.Conta> GetAllContas()
        {
            return _context.Contas.ToList();
        }
        public async Task<int> ObterUltimoNumeroConta()
        {

            var ultimaConta = await _context.Contas
                .OrderByDescending(x => x.NumeroConta)
                .FirstOrDefaultAsync();

            if (ultimaConta == null) return 0;

            return ultimaConta.NumeroConta;
        }

        public void DeletarConta(Domain.Entities.Conta conta)
        {
            _context.Contas.Remove(conta);
        }

        public async Task<Domain.Entities.Conta?> ObterConta(Guid userIdentifier, int numeroConta)
        {
            return await _context.Contas.Where(x => x.UserIdentifier.Equals(userIdentifier)
            && x.NumeroConta == numeroConta).FirstOrDefaultAsync();

        }
        public async Task<Domain.Entities.Conta?> ObterConta(int numeroConta, int numeroBanco, int numeroAgencia)
        {
            return await _context.Contas.FirstOrDefaultAsync(x => x.NumeroConta == numeroConta &&
            x.NumeroBanco == numeroBanco &&
            x.NumeroAgencia == numeroAgencia);
        }

        public async Task<Domain.Entities.Cliente?> ObterClienteConta(int numeroConta)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.NumeroConta == numeroConta);
        }

        public void Atualizar(Domain.Entities.Conta conta)
        {
            _context.Contas.Update(conta); 
        }

        public async Task<IDbContextTransaction> ComecarTransacaoAsync()
        {
           return await _context.Database.BeginTransactionAsync();
        }
    }
}