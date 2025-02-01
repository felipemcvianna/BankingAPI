using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;

namespace Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaEMail;

public interface IAtualizarSenhaClienteUseCase
{
    public Task<ResponseAtualizarClienteJson> Execute(RequestAtualizarSenhaClienteJson request);
}