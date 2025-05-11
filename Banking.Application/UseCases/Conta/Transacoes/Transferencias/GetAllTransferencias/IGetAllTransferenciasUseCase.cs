using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetAllTransferencias;

public interface IGetAllTransferenciasUseCase
{
    Task<List<ResponseExecutarTransferenciaJson>> Execute();
}