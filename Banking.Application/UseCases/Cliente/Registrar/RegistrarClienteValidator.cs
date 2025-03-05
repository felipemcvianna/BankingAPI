using Banking.Communication.Requests.Cliente;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.Registrar;

public class RegistrarClienteValidator : AbstractValidator<RequestRegistrarClienteJson>
{
    public RegistrarClienteValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage(ResourceMessagesExceptions.NOME_VAZIO);
        RuleFor(x => x.CPF.Length).GreaterThanOrEqualTo(11).WithMessage(ResourceMessagesExceptions.CPF_INVALIDO);
        RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_VAZIO);
        When(x => !string.IsNullOrEmpty(x.Email),
            () => { RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALIDO); }
        );
        RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
    }
}