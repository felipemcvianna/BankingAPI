using Banking.Domain.Repositories.Cliente;
using Microsoft.EntityFrameworkCore;
using System;

namespace Banking.Infrastructure.Data.Repositories.Cliente;

public class ClienteRepository : IGravarClienteRepository, ILerCLienteRepository
{
    private readonly BankingDbContext _context;

    public ClienteRepository(BankingDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.Cliente cliente) => await _context.Clientes.AddAsync(cliente);

    public async Task<Domain.Entities.Cliente> GetClienteByEmail(string email)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Email.Equals(email));

        return cliente!;
    }

    public async Task<bool> ExisteClienteComCpf(string cpf)
        => await _context.Clientes.AnyAsync(x => x.CPF.Equals(cpf));

    public async Task<bool> ExisteClienteComEmail(int? id, string email)
        => await _context.Clientes.AnyAsync(x =>
            (!string.IsNullOrEmpty(email) && x.Email.Equals(email)) || (id != null && x.Id == id));
}