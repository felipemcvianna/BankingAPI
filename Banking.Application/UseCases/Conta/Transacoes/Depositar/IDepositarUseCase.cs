using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Transacao.Depositar
{
    public interface IDepositarUseCase
    {
        public Task<ResponseDepositarJson> Execute(RequestExecutarTransacaoJson request);
    }
}
