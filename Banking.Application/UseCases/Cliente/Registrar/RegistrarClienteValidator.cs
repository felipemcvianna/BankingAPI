using Banking.Communication.Requests.Cliente;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.Registrar;

public class RegistrarClienteValidator : AbstractValidator<RequestRegistrarClienteJson>
{
    public RegistrarClienteValidator()
    {
        RuleFor(x => x.Nome.Length).NotEmpty().WithMessage("ERROR, NOME VAZIO");
        RuleFor(x => x.Email).EmailAddress().WithMessage("FORMATO INCORRETO");
        RuleFor(x => x.CPF.Length).NotEqual(11).WithMessage("SENHA DO TAMANHO INVÁLIDO");
        When(x => string.IsNullOrEmpty(x.Email) == false,
            () => { RuleFor(x => x.Email).NotEmpty().WithMessage("efevervrever"); }
        );
        RuleFor(x => x.Senha).NotEqual(4).WithMessage("SENHA PEQUENA DEMAIS");
    }
}