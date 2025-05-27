using Banking.Communication.Requests.Conta.Transferencia;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetTransferenciaByData;

public interface IGetTransferenciaByDataUseCase
{
    public Task<List<ResponseExecutarTransferenciaJson>> Execute(RequestGetTransferenciaByData request);
}