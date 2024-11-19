using Banking.Communication.Requests.Cliente;
using Bogus;

namespace CommomTestUtilities.Requests;

public static class RequestLerClienteJsonBuilder
{
    public static RequestGetCliente Build()
    {
        return new Faker<RequestGetCliente>()
            .RuleFor(x => x.Email, (f) => f.Internet.Email());
    }
}