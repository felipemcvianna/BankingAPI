using Banking.Communication.Token;

namespace Banking.Communication.Response.Cliente;

public class ResponseRegistrarClienteJson
{
    public string Nome { get; set; } = string.Empty;
    public ResponseTokensJson Tokens { get; set; } = null!;
}