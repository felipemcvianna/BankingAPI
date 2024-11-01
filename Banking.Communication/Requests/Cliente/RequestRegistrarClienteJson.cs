namespace Banking.Communication.Requests.Cliente;

public class RequestRegistrarClienteJson
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public int Senha { get; set; }
}