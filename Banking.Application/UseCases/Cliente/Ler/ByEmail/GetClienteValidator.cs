using Banking.Communication.Requests.Cliente;
using Banking.Exceptions;
using FluentValidation;

namespace Banking.Application.UseCases.Cliente.Ler.ByEmail;

public class GetClienteValidator : AbstractValidator<RequestGetCliente>
{
    public GetClienteValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_VAZIO);
        When(x => !string.IsNullOrEmpty(x.Email), () =>
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALIDO);
        });
    }
}