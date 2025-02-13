namespace Banking.Domain.Entities;

public class Transferencia
{
    public int Id { get; set; }
    public string NumeroTransacao { get; set; } = default!;
    public string NomeClienteOrigem { get; set; } = default!;
    public string NomeClienteDestino { get; set; } = default!;

    public string CpfClienteOrigem { get; set; } = default!;
    public string CpfClienteDestino { get; set; } = default!;
    public AuxiliarTransacao ContaOrigem { get; set; } = default!;
    public AuxiliarTransacao ContaDestino { get; set; } = default!;
    public double ValorTransacao { get; set; } = default!;
    public DateTime DataTransacao { get; private set; } = DateTime.UtcNow;
}