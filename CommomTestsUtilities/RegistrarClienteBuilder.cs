using Banking.Communication.Requests.Cliente;
using Bogus;
using Bogus.Extensions.Brazil;

namespace CommomTestsUtilities;

public static class RegistrarClienteBuilder
{
    public static RequestRegistrarClienteJson Build()
    {
        return new Faker<RequestRegistrarClienteJson>("pt_BR")
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.CPF, f => f.Person.Cpf())
            .Generate();
    }
}