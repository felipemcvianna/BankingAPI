using Banking.Domain.Entities;

namespace Banking.Communication.Requests.Conta.Transacao
{
    public class RequestSaqueJson : AuxiliarTransacao
    {
        public double ValorTransacao { get; set; }
        public string Senha { get; set; }
    }
}