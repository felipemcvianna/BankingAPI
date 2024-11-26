using Banking.Communication.Requests.Cliente;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.Registrar;

public class RegistrarClienteValidator : AbstractValidator<RequestRegistrarClienteJson>
{
    public RegistrarClienteValidator()
    {
        RuleFor(x => x.Nome.Length).NotEmpty().WithMessage("ERROR, NOME VAZIO");
        RuleFor(x => x.CPF.Length).NotEqual(11).WithMessage("SENHA DO TAMANHO INVÁLIDO");
        When(x => !string.IsNullOrEmpty(x.Email),
            () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage("FORMATO DO EMAIL INVÁLIDO");
                
            }
        );
        RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage("A SENHA DEVE TER NO MÍNIMO 6 CARACTERES");
    }
}