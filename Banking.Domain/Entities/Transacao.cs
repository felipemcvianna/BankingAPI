namespace Banking.Domain.Entities;

public class Transacao
{
    public int Id { get; set; }
    public string numeroTransacao { get; set; } = default!;
    public string nomeClienteOrigem { get; set; } = default!;
    public string nomeClienteDestino { get; set; } = default!;

    public string CPFCliteOrigem { get; set; } = default!;
    public string CPFClienteDestino { get; set; } = default!;
    public AuxiliarTransacao contaOrigem { get; set; } = default!;
    public AuxiliarTransacao contaDestino { get; set; } = default!;
    public double valorTransacao { get; set; } = default!;    
    public DateTime DataTransacao { get; set; }
}