namespace Banking.Communication.Requests.Cliente
{
    public class RequestAtualizarSenhaClienteAutenticadoJson
    {
        public string senhaAtual { get; set; } = default!;
        public string novaSenha { get; set; } = default!;
        public string confirmarSenha { get; set; } = default!;
    }
}