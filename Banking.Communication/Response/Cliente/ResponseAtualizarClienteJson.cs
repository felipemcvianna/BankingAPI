namespace Banking.Communication.Response.Cliente;

public class ResponseAtualizarClienteJson
{
    public string Mensagem { get; set; }
    public bool Sucesso { get; set; }
    public DateTime DataDeAtualizacao { get; set; }
}