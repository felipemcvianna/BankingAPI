using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;

namespace Banking.Application.UseCases.Cliente.Deletar;

public interface IDeletarClienteUseCase
{
    public Task<ResponseDeletarClienteJson> Execute(RequestDeletarClienteJson request);
}