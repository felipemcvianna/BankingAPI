using Banking.Communication.Requests.Cliente;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.Deletar;

public class DeletarClienteValidator : AbstractValidator<RequestDeletarClienteJson>
{
    public DeletarClienteValidator()
    {
        RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
        RuleFor(x => x.confirmarSenha.Length).GreaterThanOrEqualTo(6)
            .WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
    }
}