using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;

namespace Banking.API.Token
{
    public class HttpContextTokenValue : ITokenRequest
    {

        private readonly IHttpContextAccessor _httpcontextAccessor;

        public HttpContextTokenValue(IHttpContextAccessor _httpcontext) => _httpcontextAccessor = _httpcontext;


        public string Value()
        {
            var token = _httpcontextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("", ResourceMessagesExceptions.SEM_TOKEN);

            if (!token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentNullException("", ResourceMessagesExceptions.TOKEN_INVALIDO);


            return token["Bearer".Length..].Trim();
        }
    }
}
