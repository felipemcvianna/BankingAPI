namespace Banking.Communication.Requests.Cliente;

public class RequestRegistrarClienteJson
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}