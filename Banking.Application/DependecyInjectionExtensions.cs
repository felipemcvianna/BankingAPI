using Banking.Application.Services.AutoMapper;
using Banking.Application.Services.Encryption;
using Banking.Application.UseCases.Acesso.Login;
using Banking.Application.UseCases.Cliente.AtualizarSenha;
using Banking.Application.UseCases.Cliente.Deletar;
using Banking.Application.UseCases.Cliente.Ler;
using Banking.Application.UseCases.Cliente.Registrar;
using Banking.Application.UseCases.Conta;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Application;

public static class DependecyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapper();
        services.AddUseCases();
        services.AddEncryptor();
        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        //Cliente use cases
        services.AddScoped<IRegistrarClienteUseCase, RegistrarClienteUseCase>();
        services.AddScoped<IGetClienteUseCase, GetClienteUseCase>();
        services.AddScoped<IDeletarClienteUseCase, DeletarClienteUseCase>();
        services.AddScoped<IAtualizarSenhaClienteUseCase, AtualizarSenhaClienteUseCase>();

        //Conta use cases
        services.AddScoped<IRegistrarContaUseCase, RegistrarContaUseCase>();


        //Login use cases
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        return services;
    }

    private static IServiceCollection AddEncryptor(this IServiceCollection services)
    {
        services.AddScoped(opt => new PasswordEncryptor());
        return services;
    }
}