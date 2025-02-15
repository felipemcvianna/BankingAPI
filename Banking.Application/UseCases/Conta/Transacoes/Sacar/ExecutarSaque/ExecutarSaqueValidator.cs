using Banking.Communication.Requests.Conta.Transacao;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes.Sacar.ExecutarSaque;

public class ExecutarSaqueValidator : CommomContaValidator<RequestSaqueJson>
{
    public ExecutarSaqueValidator()
    {
        RuleFor(x => x.ValorTransacao)
            .NotNull().WithMessage(ResourceMessagesExceptions.VALOR_SAQUE_VAZIO)
            .GreaterThan(0).WithMessage(ResourceMessagesExceptions.VALOR_SAQUE_INVALIDO);
        RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
    }
}