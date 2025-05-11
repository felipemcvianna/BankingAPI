using Banking.Communication.Requests.Conta.Deposito;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByPeriodo;

public class IGetDepositoByPeriodoValidator : AbstractValidator<RequestGetDepositoByPeriodoJson>
{
    public IGetDepositoByPeriodoValidator()
    {
        RuleFor(x => x.DataInicial).NotNull().WithMessage();
        RuleFor(x => x.DataFinal).NotNull().WithMessage();
    }
}