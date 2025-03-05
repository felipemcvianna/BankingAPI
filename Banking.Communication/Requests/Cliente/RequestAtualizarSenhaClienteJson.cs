namespace Banking.Communication.Requests.Cliente;

public class RequestAtualizarSenhaClienteJson
{
    public string Email { get; set; } = string.Empty;
    public string SenhaAtual { get; set; } = string.Empty;
    public string NovaSenha { get; set; } = string.Empty;
    public string ConfirmarNovaSenha { get; set; } = string.Empty;
}