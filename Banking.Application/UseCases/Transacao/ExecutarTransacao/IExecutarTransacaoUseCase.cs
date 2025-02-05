using Banking.Communication.Requests.Transacao;
using Banking.Communication.Response.Transacao;

namespace Banking.Application.UseCases.Transacao.ExecutarTransacao
{
    public interface IExecutarTransacaoUseCase
    {
        public Task<ResponseExecutarTransacaoJson> Execute(RequestExecutarTransacaoJson request);
    }
}
