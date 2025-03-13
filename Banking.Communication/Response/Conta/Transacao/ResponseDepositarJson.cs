using Banking.Domain.Entities;

namespace Banking.Communication.Response.Conta.Transacao
{
    public class ResponseDepositarJson : AuxiliarTransacao
    {
        public string CPFCliente { get; set; } = default!;
        public string NomeCliente { get; set; } = default!;
        public double ValorDeposito { get; set; } = default!;
        public string NumeroDeposito { get; set; } = default!;
        public DateTime DataDeposito { get; set; }
    }
}