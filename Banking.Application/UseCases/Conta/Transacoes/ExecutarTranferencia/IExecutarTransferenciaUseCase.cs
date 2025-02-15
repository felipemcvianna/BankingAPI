using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Transacao.ExecutarTranferencia
{
    public interface IExecutarTransferenciaUseCase
    {
        public Task<ResponseExecutarTransferenciaJson> Execute(RequestExecutarTransacaoJson request);
    }
}