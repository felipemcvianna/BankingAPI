using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.Depositar
{
    public interface IDepositarUseCase
    {
        public Task<ResponseDepositarJson> Execute(RequestExecutarTransacaoJson request);
    }
}