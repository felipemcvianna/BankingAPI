namespace Banking.Domain.Entities
{
    public class Deposito
    {
        public int Id { get; set; }
        public AuxiliarTransacao ContaDeposito { get; set; } = default!;
        public string CPFCliente { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public double ValorDeposito { get; set; } = default!;
        public string NumeroDeposito { get; set; } = default!;
    }
}