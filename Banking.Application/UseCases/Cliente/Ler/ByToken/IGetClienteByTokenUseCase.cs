using Banking.Communication.Response.Cliente;

namespace Banking.Application.UseCases.Cliente.Ler.ByToken
{
    public interface IGetClienteByTokenUseCase
    {
        public Task<ResponseGetClienteByTokenJson> Execute();
    }
}
