using AutoMapper;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Entities;

namespace Banking.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RegisterRequestToDomain();
        RegisterDomainToRequest();
        TransferenciaDomainToRequest();
        DepositoDomainToRequest();
    }

    private void RegisterRequestToDomain()
    {
        CreateMap<RequestRegistrarClienteJson, Cliente>().ForMember(x => x.Senha, opt => opt.Ignore());
    }

    private void RegisterDomainToRequest()
    {
        CreateMap<Cliente, ResponseRegistrarClienteJson>()
            .ForMember(x => x.Nome, opt
                => opt.MapFrom(cliente => cliente.Nome));
    }

    private void TransferenciaDomainToRequest()
    {
        CreateMap<Transferencia, ResponseExecutarTransferenciaJson>();
    }

    private void DepositoDomainToRequest()
    {
        CreateMap<Deposito, ResponseDepositarJson>()
            .ForMember(response => response.numeroBanco, opt
                => opt.MapFrom(deposito => deposito.ContaDeposito.numeroBanco))
            .ForMember(response => response.numeroAgencia, opt
                => opt.MapFrom(deposito => deposito.ContaDeposito.numeroAgencia))
            .ForMember(response => response.numeroConta, opt
                => opt.MapFrom(deposito => deposito.ContaDeposito.numeroConta));
    }
}