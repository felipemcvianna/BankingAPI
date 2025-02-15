namespace Banking.Communication.Requests.Cliente;

public class RequestDeletarClienteJson
{
    public string Senha { get; set; } = default!;
    public string confirmarSenha { get; set; } = default!;
}