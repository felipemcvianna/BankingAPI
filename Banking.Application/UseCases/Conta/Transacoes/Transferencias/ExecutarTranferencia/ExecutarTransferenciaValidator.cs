using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Requests.Conta.Transferencia;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes.Transferencias.ExecutarTranferencia;

public class ExecutarTransferenciaValidator : CommomContaValidator<RequestExecutarTransferenciaJson>
{
    public ExecutarTransferenciaValidator()
    {
        RuleFor(x => x.ValorTransacao)
            .NotEmpty().WithMessage(ResourceMessagesExceptions.VALOR_TRANSFERENCIA_VAZIO)
            .Must(valor => double.TryParse(valor, out var resultado) && resultado > 0)
            .WithMessage(ResourceMessagesExceptions.VALOR_TRANSFERENCIA_INVALIDO);
    }
}