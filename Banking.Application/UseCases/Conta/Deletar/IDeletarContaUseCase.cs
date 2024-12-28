namespace Banking.Application.UseCases.Conta.Deletar
{
    public interface IDeletarContaUseCase
    {
        public Task Execute(Guid userIdentifier, int numeroConta);
    }
}
