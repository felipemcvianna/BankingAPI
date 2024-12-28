
using Banking.Domain.Repositories.Conta;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Deletar
{
    public class DeletarContaUseCase : IDeletarContaUseCase
    {
        private readonly ILerContaRepository _lerContaRepository;
        private readonly IGravarContaRepository _gravarContaRepository;

        public DeletarContaUseCase(ILerContaRepository lerContaRepository, IGravarContaRepository gravarContaRepository)
        {
            _lerContaRepository = lerContaRepository;
            _gravarContaRepository = gravarContaRepository;
        }

        public async Task Execute(Guid userIdentifier, int numeroConta)
        {
            var conta = await _lerContaRepository.ObterConta(userIdentifier, numeroConta);

            if (conta == null)
                throw new BusinessException("CONTA NÃO ENCONTRADA");

            await _gravarContaRepository.DeletarConta(conta);
        }
    }
}

