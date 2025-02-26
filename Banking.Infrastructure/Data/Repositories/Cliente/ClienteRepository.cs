using Banking.Domain.Repositories.Cliente;
using Microsoft.EntityFrameworkCore;
using System;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Entities;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

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
        => await _context.Clientes.FirstOrDefaultAsync(x => x.Email.Equals(email));

    public async Task<bool> ExisteClienteComCpf(string cpf)
        => await _context.Clientes.AnyAsync(x => x.CPF.Equals(cpf));

    public async Task<bool> ExisteClienteComEmail(string email)
        => await _context.Clientes.AnyAsync(x =>
            (!string.IsNullOrEmpty(email) && x.Email.Equals(email)));

    public async Task<bool> ExisteClienteComIdentificador(Guid userIdentifier)
        => await _context.Clientes.AnyAsync(c => c.UserIdentifier.Equals(userIdentifier));

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

    public async Task<Domain.Entities.Cliente?> GetClienteByNumeroConta(int numeroConta) =>
        await _context.Clientes.FirstOrDefaultAsync(x => x.NumeroConta == numeroConta);

    public async Task<ContaAndCliente> GetClienteAndConta(int numeroConta)
    {
        var clienteConta = await _context.Clientes.Include(c => c.Conta)
            .FirstOrDefaultAsync(x => x.NumeroConta == numeroConta);

        if (clienteConta == null)
        {
            throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);
        }

        return new ContaAndCliente()
        {
            Nome = clienteConta.Nome,
            CPF = clienteConta.CPF,
            Conta = clienteConta.Conta
        };
    }
}