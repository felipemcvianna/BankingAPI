using Banking.Communication.Requests.Conta.Transacao;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes.Sacar.ExecutarSaque;

public class ExecutarSaqueValidator : CommomContaValidator<RequestSaqueJson>
{
    public ExecutarSaqueValidator()
    {
        RuleFor(x => x.ValorTransacao.Length)
            .NotNull().WithMessage(ResourceMessagesExceptions.VALOR_TRANSFERENCIA_VAZIO)
            .GreaterThan(0).WithMessage(ResourceMessagesExceptions.VALOR_TRANSFERENCIA_INVALIDO);
        RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
    }
}