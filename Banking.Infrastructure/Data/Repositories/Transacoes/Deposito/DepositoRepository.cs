using System.Globalization;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Exceptions.ExceptionBase;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Data.Repositories.Transacoes.Deposito
{
    public class DepositoRepository : IGravarDepositoRepository, ILerDepositosRepository
    {
        private readonly BankingDbContext _context;

        public DepositoRepository(BankingDbContext context)
        {
            _context = context;
        }

        public async Task Add(Domain.Entities.Deposito deposito) => await _context.Depositos.AddAsync(deposito);

        public async Task<List<Domain.Entities.Deposito>> GetAllDepositos(string cpfCliente) =>
            await _context.Depositos.Where(x => x.CpfCliente == cpfCliente).ToListAsync();

        public async Task<List<Domain.Entities.Deposito>> GetDepositosByPeriodo(DateTime startDate, DateTime endDate)
        {
            var dataAux = endDate.AddDays(1);

            return await _context.Depositos
                .Where(d => d.DataDeposito >= startDate && d.DataDeposito <= dataAux)
                .ToListAsync();
        }

        public async Task<List<Domain.Entities.Deposito>> ObterDepositoByData(DateTime dataDeposito) =>
            await _context.Depositos.Where(d => d.DataDeposito.Date == dataDeposito.Date).ToListAsync();

        public async Task<Domain.Entities.Deposito?> ObterDepositoPorNumero(string numeroDeposito) =>
            await _context.Depositos.FirstOrDefaultAsync(x => x.NumeroDeposito == numeroDeposito);
    }
}