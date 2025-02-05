using Banking.Communication.Response.Conta;

namespace Banking.Communication.Response.Cliente
{
    public class ResponseGetClienteByTokenJson
    {
        public string? Email { get; set; } 
        public string Nome { get; set; } = default!;

        public ResponseGetContaByTokenJson contaCliente { get; set; } = default!;
    }
}
