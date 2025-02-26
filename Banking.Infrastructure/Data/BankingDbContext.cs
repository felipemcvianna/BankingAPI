using Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Data;

public class BankingDbContext : DbContext
{
    public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Transferencia> Transferencias { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Deposito> Depositos { get; set; }

    public DbSet<Saque> Saques { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transferencia>()
            .OwnsOne(t => t.ContaOrigem);

        modelBuilder.Entity<Transferencia>()
            .OwnsOne(t => t.ContaDestino);

        modelBuilder.Entity<Deposito>()
            .OwnsOne(t => t.ContaDeposito);

        modelBuilder.Entity<Saque>()
            .OwnsOne(t => t.ContaSaque);
        
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Conta)
            .WithOne(c => c.Cliente)
            .HasForeignKey<Cliente>(c => c.ContaId); 

    }
}