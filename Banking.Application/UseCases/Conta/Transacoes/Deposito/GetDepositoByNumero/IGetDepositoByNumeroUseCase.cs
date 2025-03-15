using Banking.Communication.Requests.Conta.Deposito;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByNumero;

public interface IGetDepositoByNumeroUseCase
{
    public Task<ResponseDepositarJson> Execute(RequestGetDepositoByNumeroJson request);
}