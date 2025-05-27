using Banking.Domain.Entities;

namespace Banking.Communication.Response.Conta.Transacao
{
    public class ResponseExecutarTransferenciaJson
    {
        public string nomeClienteOrigem { get; set; } = default!;
        public string nomeClienteDestino { get; set; } = default!;

        public string CPFClienteOrigem { get; set; } = default!;
        public string CPFClienteDestino { get; set; } = default!;
        public AuxiliarTransacao ContaOrigem { get; set; } = default!;
        public AuxiliarTransacao ContaDestino { get; set; } = default!;
        public double ValorTransacao { get; set; } = default!;
        public string NumeroTransacao { get; set; } = default!;
        public DateTime DataTransacao { get; set; }
    }
}