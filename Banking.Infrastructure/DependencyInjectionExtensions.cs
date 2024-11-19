using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Infrastructure.Data.Repositories;
using Banking.Infrastructure.Data.Repositories.Cliente;
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
        services.AddScoped<IGravarClienteRepository, ClienteRepository>();
        services.AddScoped<ILerCLienteRepository, ClienteRepository>(); 
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

}