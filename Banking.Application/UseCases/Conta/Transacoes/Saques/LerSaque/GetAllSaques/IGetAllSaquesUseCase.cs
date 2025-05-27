using Banking.Domain.Entities;

namespace Banking.Application.UseCases.Conta.Transacoes.Saques.LerSaque.GetAllSaques;

public interface IGetAllSaquesUseCase
{
    public Task<List<Saque>> Execute();
}