<<<<<<< HEAD
namespace Banking.Application.UseCases.Cliente.Deletar;

public class IDeletarClienteUseCase
{
    
=======
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;

namespace Banking.Application.UseCases.Cliente.Deletar;

public interface IDeletarClienteUseCase
{
    public Task<ResponseDeletarClienteJson> Execute(RequestDeletarClienteJson request);
>>>>>>> 3c5e9f2 (Caso de uso deletar cliente)
}