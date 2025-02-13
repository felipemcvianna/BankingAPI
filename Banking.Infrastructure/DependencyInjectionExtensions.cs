using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Repositories.Conta;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Domain.Repositories.Transacoes.Saque;
using Banking.Domain.Seguranca.Tokens;
using Banking.Domain.Seguranca.Transacoes;
using Banking.Infrastructure.Data;
using Banking.Infrastructure.Data.Repositories;
using Banking.Infrastructure.Data.Repositories.Cliente;
using Banking.Infrastructure.Data.Repositories.Conta;
using Banking.Infrastructure.Data.Repositories.Transacoes.Deposito;
using Banking.Infrastructure.Data.Repositories.Transacoes.Saque;
using Banking.Infrastructure.Seguranca.Tokens.Acesso.Generator;
using Banking.Infrastructure.Seguranca.Tokens.GetCliente;
using Banking.Infrastructure.Seguranca.Tokens.Validator;
using Banking.Infrastructure.Seguranca.Transacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastrucutre(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();
        services.AddTokens(configuration);
        services.AddLoggedCliente();
        services.AddSegurancaTransacao(configuration);
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BankingDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        //CLIENTE REPOSITORIES
        services.AddScoped<IGravarClienteRepository, ClienteRepository>();
        services.AddScoped<ILerCLienteRepository, ClienteRepository>();
        services.AddScoped<IDeletarClienteRepository, ClienteRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //CONTA REPOSITORIES
        services.AddScoped<IGravarContaRepository, ContaRepository>();
        services.AddScoped<ILerContaRepository, ContaRepository>();

        //DEPOSITO REPOSITORIES
        services.AddScoped<IGravarDepositoRepository, DepositoRepository>();

        //SAQUE REPOSITORIES
        services.AddScoped<IGravarSaqueRepository, SaqueRepository>();
        
        //TRANSFERENCIA REPOSITORIES
        return services;
    }

    private static IServiceCollection AddTokens(this IServiceCollection services, IConfiguration configuration)
    {
        var expiretionTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAcessTokenGenerator>(options =>
            new AcessTokenGenerator(signingKey!, expiretionTimeMinutes));
        services.AddScoped<IJwtTokenValidator>(options => new JwtTokenValidator(signingKey!));

        return services;
    }

    private static IServiceCollection AddSegurancaTransacao(this IServiceCollection services,
        IConfiguration configuration)
    {
        var chaveSegura = configuration.GetValue<string>("Settings:Transacoes:ChaveSegura");
        services.AddScoped<ISegurancaTransacao>(options =>
            new SegurancaTransacao($"{Guid.NewGuid()}|{DateTime.UtcNow.Ticks}", chaveSegura!));
        return services;
    }

    private static IServiceCollection AddLoggedCliente(this IServiceCollection services)
        => services.AddScoped<ILoggedCliente, LoggedCliente>();
}