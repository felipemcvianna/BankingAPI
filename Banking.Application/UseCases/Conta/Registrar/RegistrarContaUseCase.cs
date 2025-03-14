﻿using Banking.Communication.Response.Conta;
using Banking.Domain.Repositories.Conta;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Registrar
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

        public async Task<ResponseRegistrarContaJson> Execute(Guid userIdentifier)
        {
            var numeroBanco = ObterNumeroBanco();
            var numeroAgencia = ObterNumeroAgencia();
            var numeroConta = await GerarNumeroConta();

            var conta = new Domain.Entities.Conta(numeroAgencia, numeroBanco, numeroConta, userIdentifier)
            {
                NumeroAgencia = numeroAgencia,
                NumeroBanco = numeroBanco,
                NumeroConta = numeroConta,
                UserIdentifier = userIdentifier
            };

            await ValidarSeContaExiste(conta.NumeroConta);

            await _gravarRepository.Add(conta);

            return new ResponseRegistrarContaJson()
            {
                Conta = conta
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