using Banking.Communication.Requests.Conta.Transferencia;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetTransferenciaByNumero;

public class GetTransferenciaByNumeroValidator : AbstractValidator<RequestGetTransferenciaByNumeroJson>
{
    public GetTransferenciaByNumeroValidator()
    {
        RuleFor(r => r.NumeroTransferencia).NotEmpty()
            .WithMessage(ResourceMessagesExceptions.NUMERO_TRANS_VAZIO);

        When(c => !string.IsNullOrEmpty(c.NumeroTransferencia),
            () =>
            {
                RuleFor(c => c.NumeroTransferencia).Length(16)
                    .WithMessage(ResourceMessagesExceptions.NUMERO_TRANS_INVALIDO);
            });
    }
}