using Banking.Application.Services.Transacao;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Domain.Seguranca.Transacoes;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Transacao.Depositar
{
    public class DepositarUseCase : IDepositarUseCase
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IGravarDepositoRepository _gravarDepositoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISegurancaTransacao _segurancaTransacao;

        public DepositarUseCase(ITransacaoService transacaoService,
            IGravarDepositoRepository gravarDepositoRepository,
            IUnitOfWork unitOfWork,
            ISegurancaTransacao segurancaTransacao)
        {
            _transacaoService = transacaoService;
            _gravarDepositoRepository = gravarDepositoRepository;
            _unitOfWork = unitOfWork;
            _segurancaTransacao = segurancaTransacao;
        }

        public async Task<ResponseDepositarJson> Execute(RequestExecutarTransacaoJson request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "A requisição não pode ser nula.");

            if (!double.TryParse(request.valorTransacao, out double valor) || valor <= 0)
                throw new BusinessException("O valor da transação é inválido.");

            var conta = await _transacaoService.ObterConta(request.numeroConta, request.numeroBanco, request.numeroAgencia);

            var cliente = await _transacaoService.ObterClienteByNumeroConta(conta.NumeroConta);

            _transacaoService.ExecutarDeposito(conta, valor);

            var deposito = new Domain.Entities.Deposito
            {
                CPFCliente = cliente.CPF,
                Nome = cliente.Nome,
                ContaDeposito = new Domain.Entities.AuxiliarTransacao
                {
                    numeroAgencia = conta.NumeroAgencia,
                    numeroConta = conta.NumeroConta,
                    numeroBanco = conta.NumeroBanco
                },
                ValorDeposito = valor,
                NumeroDeposito = _segurancaTransacao.GerarNumeroTransacao()
            };

            await _gravarDepositoRepository.Add(deposito);
            await _unitOfWork.Commit();

            return new ResponseDepositarJson
            {
                CPFCliente = deposito.CPFCliente,
                Nome = deposito.Nome,
                numeroConta = deposito.ContaDeposito.numeroConta,
                numeroAgencia = deposito.ContaDeposito.numeroAgencia,
                numeroBanco = deposito.ContaDeposito.numeroBanco,
                ValorDeposito = valor,
                NumeroDeposito = deposito.NumeroDeposito
            };
        }

    }
}
