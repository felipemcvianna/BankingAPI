using Banking.Domain.Entities;

namespace Banking.Application.UseCases.Conta.Transacoes.Sacar.LerSaque.GetAllSaques;

public interface IGetAllSaquesUseCase
{
    public Task<List<Saque>> Execute();
}