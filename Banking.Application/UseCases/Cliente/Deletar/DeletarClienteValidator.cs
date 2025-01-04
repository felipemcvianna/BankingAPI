using Banking.Communication.Requests.Cliente;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.Deletar;

public class DeletarClienteValidator : AbstractValidator<RequestDeletarClienteJson>
{
    public DeletarClienteValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_VAZIO);
        When(x => !string.IsNullOrEmpty(x.Email),
            () => { RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALIDO); });
        RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
    }
}