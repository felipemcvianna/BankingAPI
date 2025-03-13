using Banking.Communication.Requests.Cliente;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaEMail;

public class AtualizarSenhaValidator : AbstractValidator<RequestAtualizarSenhaClienteJson>
{
    public AtualizarSenhaValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_VAZIO)
            .EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALIDO);

        RuleFor(x => x.SenhaAtual)
            .NotEmpty().WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
            .MinimumLength(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);

        RuleFor(x => x.NovaSenha)
            .NotEmpty().WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
            .MinimumLength(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
            .NotEqual(x => x.SenhaAtual).WithMessage(ResourceMessagesExceptions.SENHA_IGUAL);

        RuleFor(x => x.ConfirmarNovaSenha)
            .MinimumLength(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
            .NotEmpty().WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
            .Equal(x => x.NovaSenha).WithMessage(ResourceMessagesExceptions.SENHAS_DEVEM_COINCIDIR);
    }

}