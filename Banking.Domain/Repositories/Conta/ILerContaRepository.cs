namespace Banking.Domain.Repositories.Conta
{
    public interface ILerContaRepository
    {
        public Task<bool> ExisteConta(int numeroConta);
        public Task<int> ObterUltimoNumeroConta();
    }
}
