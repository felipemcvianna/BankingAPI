namespace Banking.Domain.Entities;

public class Transacao
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public double Valor { get; set; }
    public DateTime DataeHora { get; set; }
}