using Banking.Domain.Entities;

namespace Banking.Communication.Requests.Transacao
{
    public class RequestExecutarTransacaoJson : AuxiliarTransacao
    {
        public double valorTransacao { get; set; }
    }
}
