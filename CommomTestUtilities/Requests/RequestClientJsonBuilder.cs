using Banking.Communication.Requests.Cliente;
using Bogus;
using Bogus.Extensions.Brazil;

namespace CommomTestUtilities.Requests;

public static class RequestClientJsonBuilder
{
    public static RequestRegistrarClienteJson Build(int senha = 10)
    {
        return new Faker<RequestRegistrarClienteJson>()
            .RuleFor(x => x.Nome, (f) => f.Internet.UserName())
            .RuleFor(x => x.Email, (f) => f.Internet.Email())
            .RuleFor(x => x.Senha, (f) => f.Internet.Password(senha))
            .RuleFor(x => x.CPF, (f) => f.Person.Cpf());
    }
}