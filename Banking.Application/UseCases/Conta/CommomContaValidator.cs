using Banking.Domain.Entities;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes;

public abstract class CommomContaValidator : AbstractValidator<AuxiliarTransacao>
{
    protected CommomContaValidator()
    {
        RuleFor(x => x.numeroAgencia).NotEmpty().WithMessage("");
        RuleFor(x => x.numeroConta).NotEmpty().WithMessage("");
        RuleFor(x => x.numeroBanco).NotEmpty().WithMessage("");
    }
}