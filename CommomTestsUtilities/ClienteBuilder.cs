using Banking.Communication.Requests.Cliente;
using Bogus;
using Bogus.Extensions.Brazil;

namespace CommomTestsUtilities;

public static class ClienteBuilder
{
    public static RequestRegistrarClienteJson RegistrarClienteBuild(int tamanhoSenha = 10)
    {
        return new Faker<RequestRegistrarClienteJson>("pt_BR")
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => f.Random.AlphaNumeric(tamanhoSenha))
            .RuleFor(c => c.CPF, f => f.Person.Cpf())
            .Generate();
    }

    public static RequestAtualizarSenhaClienteJson AtualizarSenhaBuild(int tamanhoSenha = 10)
    {
        return new Faker<RequestAtualizarSenhaClienteJson>("pt_BR")
            .RuleFor(e => e.Email, f => f.Internet.Email())
            .RuleFor(e => e.NovaSenha, f => f.Random.AlphaNumeric(tamanhoSenha))
            .RuleFor(e => e.SenhaAtual, f => f.Random.AlphaNumeric(tamanhoSenha));
    }
}