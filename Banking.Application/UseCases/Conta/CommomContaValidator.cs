using Banking.Domain.Entities;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Conta;

public class CommomContaValidator<T> : AbstractValidator<T> where T : AuxiliarTransacao
{
    protected CommomContaValidator()
    {
        RuleFor(x => x.numeroAgencia).NotEmpty().WithMessage(ResourceMessagesExceptions.NUMERO_AGENCIA_VAZIO);
        RuleFor(x => x.numeroConta).NotEmpty().WithMessage(ResourceMessagesExceptions.NUMERO_CONTA_VAZIO);
        RuleFor(x => x.numeroBanco).NotEmpty().WithMessage(ResourceMessagesExceptions.NUMERO_BANCO_VAZIO);
    }
}