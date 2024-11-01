namespace Banking.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public int ContaId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public int Senha { get; set; }
}