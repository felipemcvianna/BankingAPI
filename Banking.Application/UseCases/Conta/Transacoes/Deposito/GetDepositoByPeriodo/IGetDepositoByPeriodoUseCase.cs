using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByPeriodo;

public interface IGetDepositoByPeriodoUseCase
{
    public Task<List<ResponseDepositarJson>> Execute(RequestGetDepositoByPeriodo request);
}