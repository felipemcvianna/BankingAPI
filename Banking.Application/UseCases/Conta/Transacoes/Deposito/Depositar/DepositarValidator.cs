using Banking.Communication.Requests.Conta.Transacao;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.Depositar;

public class DepositarValidator : CommomContaValidator<RequestExecutarTransacaoJson>
{
    public DepositarValidator()
    {
        RuleFor(x => x.valorTransacao).NotEmpty().WithMessage(ResourceMessagesExceptions.VALOR_TRANSFERENCIA_VAZIO)
            .Must(valor => double.TryParse(valor, out var resultado) && resultado > 0)
            .WithMessage(ResourceMessagesExceptions.VALOR_TRANSFERENCIA_INVALIDO);
    }
}