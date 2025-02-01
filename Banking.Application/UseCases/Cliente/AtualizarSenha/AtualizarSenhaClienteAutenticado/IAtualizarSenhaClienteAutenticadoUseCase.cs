using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;

namespace Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaClienteAutenticado
{
    public interface IAtualizarSenhaClienteAutenticadoUseCase
    {
        public Task<ResponseAtualizarClienteJson> Execute(RequestAtualizarSenhaClienteAutenticadoJson request);
    }
}
