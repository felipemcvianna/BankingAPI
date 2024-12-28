using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Communication.Requests.Login;
using FluentValidation;

namespace Banking.Application.UseCases.Acesso.Login
{
    public class LoginValidator : AbstractValidator<RequestLoginJson>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("O CAMPO EMAIL DEVE SER PREENCHIDO");
            When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage("EMAIL INVÁLIDO");
            });
            RuleFor(x => x.Senha.Length).GreaterThanOrEqualTo(6).WithMessage("A SENHA DEVE TER MAIS DE 6 CARACTERES");
        }
    }
}
