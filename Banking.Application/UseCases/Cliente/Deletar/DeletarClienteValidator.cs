<<<<<<< HEAD
namespace Banking.Application.UseCases.Cliente.Deletar;

public class DeletarClienteValidator
{
    
=======
using Banking.Communication.Requests.Cliente;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.Deletar;

public class DeletarClienteValidator : AbstractValidator<RequestDeletarClienteJson>
{
    public DeletarClienteValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(@"O CAMPO ""EMAIL"" DEVE SER PREENCHIDO");
        When(x => !string.IsNullOrEmpty(x.Email),
            () => { RuleFor(x => x.Email).EmailAddress().WithMessage("FORMATO DO EMAIL INVÁLIDO"); });
        RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage("A SENHA DEVE TER MAIS DE 6 CARACTERES");
    }
>>>>>>> 3c5e9f2 (Caso de uso deletar cliente)
}