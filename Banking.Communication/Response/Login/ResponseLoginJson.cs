using Banking.Communication.Token;

namespace Banking.Communication.Response.Login
{
    public class ResponseLoginJson
    {
        public string Nome { get; set; } = default!;
        public ResponseTokensJson Tokens { get; set; } = default!;
    }
}