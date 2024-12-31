using Bogus;
using System.Net.Http;
using System.Text;
using System.Text.Json;

class ClienteFakeSeeder
{
    private static async Task AddFakeClientes(int count)
    {
        var faker = new Faker("pt_BR");

        var fakeClientes = new Faker<object>()
            .RuleFor("Nome", f => f.Person.FullName)
            .RuleFor("Email", f => f.Internet.Email())
            .RuleFor("Senha", f => f.Internet.Password(8))
            .RuleFor("NumeroConta", f => f.Random.Int(10000, 99999));

        var httpClient = new HttpClient();
        string apiEndpoint = "http://localhost:5000/Cliente/RegistrarCliente"; // Alterar para o endpoint correto

        for (int i = 0; i < count; i++)
        {
            var cliente = fakeClientes.Generate();

            var clienteJson = JsonSerializer.Serialize(cliente);

            var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Cliente {i + 1} enviado com sucesso.");
            }
            else
            {
                Console.WriteLine($"Erro ao enviar cliente {i + 1}: {response.ReasonPhrase}");
            }
        }
    }

    static async Task Main(string[] args)
    {
        await AddFakeClientes(100);
    }
}
