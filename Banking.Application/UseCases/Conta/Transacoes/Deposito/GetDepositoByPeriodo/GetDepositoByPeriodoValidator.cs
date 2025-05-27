using Banking.Communication.Requests.Conta.Deposito;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByPeriodo;

public class GetDepositoByPeriodoValidator : AbstractValidator<RequestGetDepositoByPeriodoJson>
{
    public GetDepositoByPeriodoValidator()
    {
        RuleFor(x => x.DataInicial).NotNull().WithMessage(ResourceMessagesExceptions.DATA_VAZIA);
        RuleFor(x => x.DataFinal).NotNull().WithMessage(ResourceMessagesExceptions.DATA_VAZIA);
    }
}