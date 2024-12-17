using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Conta;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Conta;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta
{
    public class RegistrarContaUseCase : IRegistrarContaUseCase
    {
        private readonly IGravarContaRepository _gravarRepository;
        private readonly ILerContaRepository _lerContaRepository;      

        public RegistrarContaUseCase(IGravarContaRepository gravarRepository,
            ILerContaRepository lerContaRepository)
        {
            _gravarRepository = gravarRepository;
            _lerContaRepository = lerContaRepository;
            
        }

        public async Task<ResponseRegistrarContaJson> Execute()
        {          
            var numeroBanco = ObterNumeroBanco();
            var numeroAgencia = ObterNumeroAgencia();
            var numeroConta = await GerarNumeroConta();
            
            var conta = new Domain.Entities.Conta
            {
                DataCriacao = DateTime.UtcNow,
                NumeroAgencia = numeroAgencia,
                NumeroBanco = numeroBanco,
                NumeroConta = numeroConta,
                Saldo = 0
            };
       
            await ValidarSeContaExiste(conta.NumeroConta);
            
            await _gravarRepository.Add(conta);

            return new ResponseRegistrarContaJson()
            {
                NumeroConta = conta.NumeroConta
            };
        }

        private async Task ValidarSeContaExiste(int numeroConta)
        {
            var contaExistente = await _lerContaRepository.ExisteConta(numeroConta);
            if (contaExistente)
                throw new BusinessException("CONTA JÁ EXISTENTE");
        }

        private int ObterNumeroBanco()
        {
            return 260; 
        }

        private int ObterNumeroAgencia()
        {
            return 0001;
        }

        private async Task<int> GerarNumeroConta()
        {          
            var ultimoNumero = await _lerContaRepository.ObterUltimoNumeroConta();
            return ultimoNumero + 1;
        }
    }
}
