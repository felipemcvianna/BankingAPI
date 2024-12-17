using Banking.Domain.Entities;

namespace Banking.Domain.Repositories.Conta
{
    public interface IGravarContaRepository
    {
        public Task Add(Domain.Entities.Conta conta);
    }
}
