using Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure;

public class BankingDbContext : DbContext
{
    public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
    { 
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Deposito> Depositos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transacao>()
            .OwnsOne(t => t.contaOrigem);

        modelBuilder.Entity<Transacao>()
            .OwnsOne(t => t.contaDestino);

        modelBuilder.Entity<Deposito>()
            .OwnsOne(t => t.ContaDeposito);
    }

}