
using Microsoft.EntityFrameworkCore.Storage;


namespace Banking.Domain.Repositories.Conta
{
    public interface IGravarContaRepository
    {
        public Task Add(Entities.Conta conta);
        public void DeletarConta(Entities.Conta conta);

        public Task Atualizar(Entities.Conta conta);

        public Task<IDbContextTransaction> ComecarTransacaoAsync();
    }
}
