namespace Banking.Domain.Entities;

public class Transferencia
{
    public int Id { get; set; }
    public string NumeroTransacao { get; set; } = default!;
    public int IdClienteOrigem { get; set; }
    public Cliente ClienteOrigem { get; set; } = default!;
    public int IdClienteDestino { get; set; }
    public Cliente ClienteDestino { get; set; } = default!;
    public double ValorTransacao { get; set; }
    public DateTime DataTransacao { get; set; }
}