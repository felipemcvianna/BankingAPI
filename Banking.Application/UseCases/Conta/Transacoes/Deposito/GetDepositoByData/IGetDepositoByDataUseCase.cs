using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByData;

public interface IGetDepositoByDataUseCase
{
    public Task<List<ResponseDepositarJson>> Execute(RequestGetDepositoByData request);
}