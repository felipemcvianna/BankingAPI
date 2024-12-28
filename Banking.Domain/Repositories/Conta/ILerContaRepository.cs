namespace Banking.Domain.Repositories.Conta
{
    public interface ILerContaRepository
    {
        public Task<bool> ExisteConta(int numeroConta);
        public Task<int> ObterUltimoNumeroConta();
        public Task<Entities.Conta?> ObterConta(Guid userIdentifier, int numeroConta);
    }
}
