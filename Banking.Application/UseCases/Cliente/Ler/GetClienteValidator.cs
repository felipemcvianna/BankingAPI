using System.Globalization;
using Banking.Communication.Requests.Cliente;
using Banking.Domain.Repositories.Cliente;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.Ler;

public class GetClienteValidator : AbstractValidator<RequestGetCliente>
{
    public GetClienteValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(@"O CAMPO ""EMAIL"" DEVE SER PREENCHIDO");
            When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage("FORMATO DO EMAIL INVÁLIDO");
            });
    }
}