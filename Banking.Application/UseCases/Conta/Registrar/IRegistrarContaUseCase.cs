using Banking.Communication.Response.Conta;

namespace Banking.Application.UseCases.Conta.Registrar
{
    public interface IRegistrarContaUseCase
    {
        public Task<ResponseRegistrarContaJson> Execute(Guid userIdentifier);
    }
}