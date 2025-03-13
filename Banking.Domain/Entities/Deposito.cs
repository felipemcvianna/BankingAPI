namespace Banking.Domain.Entities
{
    public class Deposito
    {
        public int Id { get; set; }
        public AuxiliarTransacao ContaDeposito { get; set; } = default!;
        public string CpfCliente { get; set; } = default!;
        public string NomeCliente { get; set; } = default!;
        public double ValorDeposito { get; set; } = default!;
        public string NumeroDeposito { get; set; } = default!;
        public DateTime DataDeposito { get; set; }
    }
}