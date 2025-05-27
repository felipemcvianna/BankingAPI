using Banking.Domain.Entities;

namespace Banking.Communication.Requests.Conta.Transferencia
{
    public class RequestExecutarTransferenciaJson : AuxiliarTransacao
    {
        public string ValorTransacao { get; set; } = default!;
    }
}