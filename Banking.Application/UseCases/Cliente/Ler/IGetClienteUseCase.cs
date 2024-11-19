using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;

namespace Banking.Application.UseCases.Cliente.Ler;

public interface IGetClienteUseCase
{
    public Task<ResponseGetClienteJson> Execute(RequestGetCliente request);
}