using Banking.Communication.Requests.Conta.Transacao;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Conta.Transacoes.Saques.Sacar;

public class ExecutarSaqueValidator : CommomContaValidator<RequestSaqueJson>
{
    public ExecutarSaqueValidator()
    {
        RuleFor(x => x.ValorTransacao)
            .NotEmpty().WithMessage(ResourceMessagesExceptions.VALOR_TRANSFERENCIA_VAZIO)
            .Must(valor => double.TryParse(valor, out var resultado) && resultado > 0)
            .WithMessage(ResourceMessagesExceptions.VALOR_TRANSFERENCIA_INVALIDO);
        RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
    }
}