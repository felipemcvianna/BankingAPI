using Banking.Communication.Requests.Cliente;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.AtualizarSenha;

public class AtualizarSenhaValidator : AbstractValidator<RequestAtualizarSenhaClienteJson>
{
    public AtualizarSenhaValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_VAZIO);
        When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALIDO);
                
            });
        RuleFor(x => x.SenhaAtual.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
        RuleFor(x => x.NovaSenha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
        RuleFor(x => x.ConfirmarNovaSenha.Length).GreaterThanOrEqualTo(6)
            .WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
    }
}