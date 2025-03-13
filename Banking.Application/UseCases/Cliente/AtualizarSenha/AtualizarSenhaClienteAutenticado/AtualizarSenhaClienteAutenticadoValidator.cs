using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Communication.Requests.Cliente;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaClienteAutenticado
{
    public class
        AtualizarSenhaClienteAutenticadoValidator : AbstractValidator<RequestAtualizarSenhaClienteAutenticadoJson>
    {
        public AtualizarSenhaClienteAutenticadoValidator()
        {
            RuleFor(x => x.senhaAtual)
                .NotEmpty().WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
                .MinimumLength(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);

            RuleFor(x => x.novaSenha)
                .NotEmpty().WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
                .MinimumLength(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
                .NotEqual(x => x.senhaAtual).WithMessage(ResourceMessagesExceptions.SENHA_IGUAL);

            RuleFor(x => x.confirmarSenha)
                .NotEmpty().WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
                .MinimumLength(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA)
                .Equal(x => x.novaSenha).WithMessage(ResourceMessagesExceptions.SENHAS_DEVEM_COINCIDIR);
        }
    }
}