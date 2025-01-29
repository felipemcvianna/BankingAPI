using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;

namespace Banking.Application.UseCases.Cliente.Ler.ByEmail;

public interface IGetClienteUseCase
{
    public Task<ResponseGetClienteJson> Execute(RequestGetCliente request);
}