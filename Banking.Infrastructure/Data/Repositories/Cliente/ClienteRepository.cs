using Banking.Domain.Repositories.Cliente;
using Microsoft.EntityFrameworkCore;
using System;

namespace Banking.Infrastructure.Data.Repositories.Cliente;

public class ClienteRepository : IGravarClienteRepository, ILerCLienteRepository, IDeletarClienteRepository
{
    private readonly BankingDbContext _context;

    public ClienteRepository(BankingDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.Cliente cliente) => await _context.Clientes.AddAsync(cliente);

    public void AtualizarSenhaCliente(Domain.Entities.Cliente cliente) =>
        _context.Entry(cliente).Property(x => x.Senha).IsModified = true;


    public async Task<Domain.Entities.Cliente?> GetClienteByEmail(string email)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Email.Equals(email));

        return cliente;
    }

    public async Task<bool> ExisteClienteComCpf(string cpf)
        => await _context.Clientes.AnyAsync(x => x.CPF.Equals(cpf));

    public async Task<bool> ExisteClienteComEmail(int? id, string email)
        => await _context.Clientes.AnyAsync(x =>
            (!string.IsNullOrEmpty(email) && x.Email.Equals(email)) || (id != null && x.Id == id));


    public void Deletar(Domain.Entities.Cliente cliente)
    {
        try
        {
            if (_context.Entry(cliente).State == EntityState.Detached)
            {
                _context.Clientes.Attach(cliente);
            }

            _context.Clientes.Remove(cliente);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao tentar deletar o cliente. Verifique dependências.", ex);
        }
    }
}