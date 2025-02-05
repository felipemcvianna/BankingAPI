using Banking.Domain.Entities;

namespace Banking.Communication.Response.Transacao
{
    public class ResponseExecutarTransacaoJson
    {
        public string nomeClienteOrigem { get; set; } = default!;
        public string nomeClienteDestino { get; set; } = default!;

        public string CPFCliteOrigem { get; set; } = default!;
        public string CPFClienteDestino { get; set; } = default!;
        public AuxiliarTransacao contaOrigem { get; set; } = default!;
        public AuxiliarTransacao contaDestino { get; set; } = default!;
        public double valorTransacao { get; set; }
    }
}
