using Banking.Communication.Token;

namespace Banking.Communication.Response.Cliente;

public class ResponseRegistrarClienteJson
{
    public string Nome { get; set; }
    public ResponseTokensJson Tokens { get; set; }
}