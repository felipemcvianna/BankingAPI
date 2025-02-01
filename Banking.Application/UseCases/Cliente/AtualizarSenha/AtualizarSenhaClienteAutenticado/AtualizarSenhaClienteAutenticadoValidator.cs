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
    public class AtualizarSenhaClienteAutenticadoValidator : AbstractValidator<RequestAtualizarSenhaClienteAutenticadoJson>
    {
        public AtualizarSenhaClienteAutenticadoValidator()
        {
            RuleFor(x => x.senhaAtual.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
            RuleFor(x => x.novaSenha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
            RuleFor(x => x.confirmarSenha.Length).GreaterThanOrEqualTo(6)
                .WithMessage(ResourceMessagesExceptions.SENHA_VAZIA);
        }
    }
}
