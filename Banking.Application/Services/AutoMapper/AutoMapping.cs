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
}