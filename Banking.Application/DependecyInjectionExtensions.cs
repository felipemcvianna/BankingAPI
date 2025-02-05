using Banking.Application.Services.AutoMapper;
using Banking.Application.Services.Encryption;
using Banking.Application.UseCases.Acesso.Login;
using Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaClienteAutenticado;
using Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaEMail;
using Banking.Application.UseCases.Cliente.Deletar;
using Banking.Application.UseCases.Cliente.Ler.ByEmail;
using Banking.Application.UseCases.Cliente.Ler.ByToken;
using Banking.Application.UseCases.Cliente.Registrar;
using Banking.Application.UseCases.Conta.Deletar;
using Banking.Application.UseCases.Conta.Registrar;
using Banking.Application.UseCases.Transacao.ExecutarTransacao;
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
        services.AddScoped<IGetClienteByTokenUseCase, GetClienteByTokenUseCase>();
        services.AddScoped<IAtualizarSenhaClienteAutenticadoUseCase, AtualizarSenhaClienteAutenticadoUseCase>();

        //Conta use cases
        services.AddScoped<IRegistrarContaUseCase, RegistrarContaUseCase>();
        services.AddScoped<IDeletarContaUseCase, DeletarContaUseCase>();


        //Login use cases
        services.AddScoped<ILoginUseCase, LoginUseCase>();

        //Transferencia Use Case
        services.AddScoped<IExecutarTransacaoUseCase, ExecutarTransacaoUseCase>();
        services.AddScoped<ITransacaoService, TransacaoService>();
        return services;
    }

    private static IServiceCollection AddEncryptor(this IServiceCollection services)
    {
        services.AddScoped(opt => new PasswordEncryptor());
        return services;
    }
}