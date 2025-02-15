namespace Banking.Communication.Response.Conta.Transacao
{
    public class ResponseSaqueJson
    {
        public double ValorSaque { get; set; } = default!;
        public DateTime DataSaque { get; set; } = DateTime.UtcNow;
        public string NumeroTransacao { get; set; } = default!;
        public double SaldoAtual { get; set; }
    }
}