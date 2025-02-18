using AutoMapper;
using Banking.Application.Services.Transacao;
using Banking.Application.UseCases.Transacao.Depositar;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Domain.Seguranca.Transacoes;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Depositar
{
    public class DepositarUseCase : IDepositarUseCase
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IGravarDepositoRepository _gravarDepositoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISegurancaTransacao _segurancaTransacao;
        private readonly IMapper _mapper;

        public DepositarUseCase(ITransacaoService transacaoService,
            IGravarDepositoRepository gravarDepositoRepository,
            IUnitOfWork unitOfWork,
            ISegurancaTransacao segurancaTransacao, IMapper mapper)
        {
            _transacaoService = transacaoService;
            _gravarDepositoRepository = gravarDepositoRepository;
            _unitOfWork = unitOfWork;
            _segurancaTransacao = segurancaTransacao;
            _mapper = mapper;
        }

        public async Task<ResponseDepositarJson> Execute(RequestExecutarTransacaoJson request)
        {
            await Validate(request);

            var conta = await _transacaoService.ObterConta(request.numeroConta, request.numeroBanco,
                request.numeroAgencia);

            var valorTransacao = double.Parse(request.valorTransacao);

            var cliente = await _transacaoService.ObterClienteByNumeroConta(conta.NumeroConta);

            _transacaoService.ExecutarDeposito(conta, valorTransacao);

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
                ValorDeposito = valorTransacao,
                NumeroDeposito = _segurancaTransacao.GerarNumeroTransacao()
            };

            await _gravarDepositoRepository.Add(deposito);

            conta.AdicionarDeposito(deposito);

            await _unitOfWork.Commit();

            return _mapper.Map<ResponseDepositarJson>(deposito);
        }

        private async Task Validate(RequestExecutarTransacaoJson request)
        {
            var validator = new DepositarValidator();

            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new BusinessException(result.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }
    }
}