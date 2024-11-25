namespace Banking.Communication.Requests.Cliente;

public class RequestAtualizarSenhaClienteJson
{
    public string Email { get; set; }
    public string SenhaAtual { get; set; }
    public string NovaSenha { get; set; }
    public string ConfirmarNovaSenha { get; set; }
}