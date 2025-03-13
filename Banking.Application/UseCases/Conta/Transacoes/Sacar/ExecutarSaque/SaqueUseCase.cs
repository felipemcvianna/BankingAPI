using Banking.Application.Services.Encryption;
using Banking.Application.Services.Transacao;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Entities;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Repositories.Conta;
using Banking.Domain.Repositories.Transacoes.Saque;
using Banking.Domain.Seguranca.Transacoes;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Sacar.ExecutarSaque
{
    public class SaqueUseCase : ISaqueUseCase
    {
        private readonly ISegurancaTransacao _segurancaTransacao;
        private readonly ITransacaoService _transacaoService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordEncryptor _encryptor;
        private readonly IGravarContaRepository _gravarContaRepository;
        private readonly IGravarSaqueRepository _gravarSaqueRepository;
        private readonly ILerCLienteRepository _clienteRepository;

        public SaqueUseCase(ISegurancaTransacao segurancaTransacao,
            ITransacaoService transacaoService, IUnitOfWork unitOfWork,
            PasswordEncryptor encryptor, IGravarContaRepository gravarContaRepository,
            IGravarSaqueRepository gravarSaqueRepository, ILerCLienteRepository clienteRepository)
        {
            _segurancaTransacao = segurancaTransacao;
            _transacaoService = transacaoService;
            _unitOfWork = unitOfWork;
            _encryptor = encryptor;
            _gravarContaRepository = gravarContaRepository;
            _gravarSaqueRepository = gravarSaqueRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseSaqueJson> Execute(RequestSaqueJson request)
        {
            await SaqueValidator(request);

            double.TryParse(request.ValorTransacao, out var valorSaque);

            var cliente = await _clienteRepository.GetClienteByNumeroConta(request.numeroConta);

            if (cliente == null)
                throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

            if (!_encryptor.Verify(request.Senha, cliente.Senha))
                throw new BusinessException(ResourceMessagesExceptions.SENHA_INCORRETA);

            _transacaoService.ExecutarSaque(cliente.Conta, valorSaque);

            var saque = new Saque()
            {
                NumeroSaque = _segurancaTransacao.GerarNumeroTransacao(),
                ValorSaque = valorSaque,
                ContaSaque = new AuxiliarTransacao()
                {
                    numeroAgencia = cliente.Conta.NumeroAgencia,
                    numeroConta = cliente.Conta.NumeroConta,
                    numeroBanco = cliente.Conta.NumeroBanco,
                }
            };

            cliente.Conta.AdicionarSaques(saque);

            _gravarContaRepository.Atualizar(cliente.Conta);
            await _gravarSaqueRepository.Add(saque);
            await _unitOfWork.Commit();

            return new ResponseSaqueJson()
            {
                NumeroTransacao = saque.NumeroSaque,
                ValorSaque = valorSaque,
                SaldoAtual = cliente.Conta.Saldo
            };
        }

        private static async Task SaqueValidator(RequestSaqueJson request)
        {
            var validate = new ExecutarSaqueValidator();

            var result = await validate.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errorsList = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorsOnValidateExceptions(errorsList);
            }
        }
    }
}