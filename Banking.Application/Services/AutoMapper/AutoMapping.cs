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
        ContaToAuxTransacao();
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

    private void ContaToAuxTransacao()
    {
        CreateMap<Conta, AuxiliarTransacao>();
    }

    private void TransferenciaDomainToRequest()
    {
        CreateMap<Transferencia, ResponseExecutarTransferenciaJson>()
            .ForMember(x => x.ContaDestino, opt
                => opt.MapFrom(c => c.ClienteDestino.Conta))
            .ForMember(x => x.ContaOrigem, opt
                => opt.MapFrom(c => c.ClienteOrigem.Conta))
            .ForMember(x => x.nomeClienteOrigem, opt
                => opt.MapFrom(c => c.ClienteOrigem.Nome))
            .ForMember(x => x.nomeClienteDestino, opt
                => opt.MapFrom(c => c.ClienteDestino.Nome))
            .ForMember(x => x.CPFClienteDestino, opt
                => opt.MapFrom(c => c.ClienteDestino.CPF))
            .ForMember(x => x.CPFClienteOrigem, opt
                => opt.MapFrom(c => c.ClienteOrigem.CPF));
    }

    private void DepositoDomainToRequest()
    {
        CreateMap<Deposito, ResponseDepositarJson>()
            .ForMember(response => response.NumeroBanco, opt
                => opt.MapFrom(deposito => deposito.ContaDeposito.NumeroBanco))
            .ForMember(response => response.NumeroAgencia, opt
                => opt.MapFrom(deposito => deposito.ContaDeposito.NumeroAgencia))
            .ForMember(response => response.NumeroConta, opt
                => opt.MapFrom(deposito => deposito.ContaDeposito.NumeroConta))
            .ForMember(response => response.DataDeposito, opt
                => opt.MapFrom(deposito => deposito.DataDeposito.ToLocalTime()));
    }
}