namespace Banking.Communication.Response.Cliente;

public class ResponseAtualizarClienteJson
{
    public string Mensagem { get; set; } = string.Empty;
    public bool Sucesso { get; set; } = false;
    public DateTime DataDeAtualizacao { get; set; }
}