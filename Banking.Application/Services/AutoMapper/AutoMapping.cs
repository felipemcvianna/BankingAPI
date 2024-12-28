using AutoMapper;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Entities;

namespace Banking.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToRequest();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegistrarClienteJson, Cliente>().ForMember(x => x.Senha, opt => opt.Ignore());
    }

    private void DomainToRequest()
    {
        CreateMap<Cliente, ResponseRegistrarClienteJson>()
            .ForMember(x => x.Nome, opt
                => opt.MapFrom(cliente => cliente.Nome));
    }
}