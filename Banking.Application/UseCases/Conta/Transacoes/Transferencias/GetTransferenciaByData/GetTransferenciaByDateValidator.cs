using System.Globalization;
using Banking.Communication.Requests.Conta.Transferencia;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetTransferenciaByData;

public class GetTransferenciaByDateValidator : AbstractValidator<RequestGetTransferenciaByData>
{
    public GetTransferenciaByDateValidator()
    {
        RuleFor(x => x.DataTrasferencia).NotEmpty().WithMessage(ResourceMessagesExceptions.DATA_VAZIA);
        RuleFor(x => x.DataTrasferencia).Must(BeValidate).WithMessage(ResourceMessagesExceptions.DATA_FORMATO_INVALIDO);
    }

    private bool BeValidate(string dataTrasferencia)
    {
        return DateTime.TryParseExact(dataTrasferencia, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
            out _);
    }
}