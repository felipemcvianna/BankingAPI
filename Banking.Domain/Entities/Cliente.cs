namespace Banking.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public int NumeroConta { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Senha { get; set; }
    public Guid UserIdentifier { get; set; }

    public Conta Conta { get; set; }
    public int ContaId { get; set; }
}