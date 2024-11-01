using Banking.Domain.Repositories.Cliente;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Data.Repositories.Cliente;

public class ClienteRepository : IGravarClienteRepository, ILerCLienteRepository
{
    private readonly BankingDbContext _context;

    public ClienteRepository(BankingDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.Cliente cliente) => await _context.Clientes.AddAsync(cliente);

    public async Task<bool> ExisteClienteComCpf(string cpf) 
        => await _context.Clientes.AnyAsync(x => x.CPF.Equals(cpf));
    
    
}