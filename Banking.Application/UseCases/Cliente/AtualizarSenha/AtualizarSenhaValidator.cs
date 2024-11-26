using Banking.Communication.Requests.Cliente;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.AtualizarSenha;

public class AtualizarSenhaValidator : AbstractValidator<RequestAtualizarSenhaClienteJson>
{
    public AtualizarSenhaValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(@"O CAMPO ""EMAIL"" DEVE SER PREENCHIDO");
        When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage("FORMATO DO EMAIL INVÁLIDO");
                
            });
        RuleFor(x => x.SenhaAtual.Length).GreaterThanOrEqualTo(6).WithMessage("A SENHA DEVE TER MAIS DE 6 CARACTERES");
        RuleFor(x => x.NovaSenha.Length).GreaterThanOrEqualTo(6).WithMessage("A SENHA DEVE TER MAIS DE 6 CARACTERES");
        RuleFor(x => x.ConfirmarNovaSenha.Length).GreaterThanOrEqualTo(6)
            .WithMessage("A SENHA DEVE TER MAIS DE 6 CARACTERES");
    }
}