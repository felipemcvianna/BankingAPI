using Banking.Communication.Response.Conta;

namespace Banking.Communication.Response.Cliente
{
    public class ResponseGetClienteByTokenJson
    {
        public string Email { get; set; } = default!;
        public string Nome { get; set; } = default!;

        public ResponseGetContaByTokenJson contaCliente { get; set; } = default!;
    }
}
