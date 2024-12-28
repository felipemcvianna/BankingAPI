using Banking.Communication.Requests.Login;
using Banking.Communication.Response.Acesso.Login;

namespace Banking.Application.UseCases.Acesso.Login
{
    public interface ILoginUseCase
    {
        public Task<ResponseLoginJson> Execute(RequestLoginJson request);
    }
}
