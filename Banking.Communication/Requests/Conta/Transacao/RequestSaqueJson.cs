using Banking.Domain.Entities;

namespace Banking.Communication.Requests.Conta.Transacao
{
    public class RequestSaqueJson : AuxiliarTransacao
    {
        public string ValorTransacao { get; set; } = string.Empty;
        public string Senha { get; set; }= string.Empty;
    }
}