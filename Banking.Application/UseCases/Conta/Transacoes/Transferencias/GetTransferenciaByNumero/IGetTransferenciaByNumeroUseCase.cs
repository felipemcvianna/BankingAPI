using Banking.Communication.Requests.Conta.Transferencia;
using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetTransferenciaByNumero;

public interface IGetTransferenciaByNumeroUseCase
{
    public Task<ResponseExecutarTransferenciaJson> Execute(RequestGetTransferenciaByNumeroJson request);
}