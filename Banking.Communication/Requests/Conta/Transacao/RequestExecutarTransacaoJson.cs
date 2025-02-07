using Banking.Domain.Entities;

namespace Banking.Communication.Requests.Conta.Transacao
{
    public class RequestExecutarTransacaoJson : AuxiliarTransacao
    {
        public string valorTransacao { get; set; } = default!;
    }
}
