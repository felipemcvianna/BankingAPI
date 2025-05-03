using Banking.Communication.Requests.Conta.Deposito;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByData;

public interface IGetDepositoByDataUseCase
{
    public Task<List<ResponseDepositarJson>> Execute(RequestGetDepositoByDataJson request);
}