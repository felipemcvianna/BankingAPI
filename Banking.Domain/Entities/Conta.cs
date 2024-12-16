namespace Banking.Domain.Entities;

public class Conta
{
    public int Id { get; set; }
    public int NumeroAgencia {  get; set; }
    public int NumeroBanco { get; set; }
    public int NumeroConta { get; set; }
    public double Saldo { get; set; }
    public DateTime DataCriacao { get; set; }
}   