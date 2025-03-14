using Banking.Communication.Response.Conta.Transacao;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetAllDepositos;

public interface IGetAllDepositosUseCase
{
    public Task<List<ResponseDepositarJson>> Execute();
}