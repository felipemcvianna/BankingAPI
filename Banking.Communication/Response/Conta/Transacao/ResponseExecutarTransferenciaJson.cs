using Banking.Domain.Entities;

namespace Banking.Communication.Response.Conta.Transacao
{
    public class ResponseExecutarTransferenciaJson
    {
        public string nomeClienteOrigem { get; set; } = default!;
        public string nomeClienteDestino { get; set; } = default!;

        public string CPFClienteOrigem { get; set; } = default!;
        public string CPFClienteDestino { get; set; } = default!;
        public AuxiliarTransacao contaOrigem { get; set; } = default!;
        public AuxiliarTransacao contaDestino { get; set; } = default!;
        public double valorTransacao { get; set; } = default!;
        public string numeroTransacao { get; set; } = default!;
        public DateTime DataTransacao { get; set; }
    }
}