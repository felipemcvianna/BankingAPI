using Banking.Domain.Repositories.Transacoes.Deposito;

namespace Banking.Infrastructure.Data.Repositories.Transacoes.Deposito
{
    public class DepositoRepository : IGravarDepositoRepository
    {
        private readonly BankingDbContext _context;

        public DepositoRepository(BankingDbContext context)
        {
            _context = context;
        }
        public async Task Add(Domain.Entities.Deposito deposito) => await _context.Depositos.AddAsync(deposito);
    }
}
