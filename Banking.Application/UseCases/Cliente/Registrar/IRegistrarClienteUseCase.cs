using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;

namespace Banking.Application.UseCases.Cliente.Registrar;

public interface IRegistrarClienteUseCase
{
    public Task<ResponseRegistrarClienteJson> Execute(RequestRegistrarClienteJson requestRegistrarClienteJson);
}