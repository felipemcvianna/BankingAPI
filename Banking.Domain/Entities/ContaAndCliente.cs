namespace Banking.Domain.Entities;

public class ContaAndCliente
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public Domain.Entities.Conta Conta { get; set; }
}