namespace Banking.Domain.Entities;

public class Saque
{
    public int Id { get; set; }
    public AuxiliarTransacao ContaSaque { get; set; } = null!;
    public double ValorSaque { get; set; }
    public string NumeroSaque { get; set; } = null!;
    public DateTime DataSaque { get; private set; } = DateTime.UtcNow;
}