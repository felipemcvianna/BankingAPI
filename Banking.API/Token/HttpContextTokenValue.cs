using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions.ExceptionBase;

namespace Banking.API.Token
{
    public class HttpContextTokenValue : ITokenRequest
    {

        private readonly IHttpContextAccessor _httpcontextAccessor;

        public HttpContextTokenValue(IHttpContextAccessor _httpcontext) => _httpcontextAccessor = _httpcontext;


        public string Value()
        {
            var token = _httpcontextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            if (token == null || !token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                throw new BusinessException("Token vazio ou inválido");

            return token["Bearer".Length..].Trim();
        }
    }
}
