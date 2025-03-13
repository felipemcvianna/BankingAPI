using AutoMapper;
using Banking.Application.Services.Transacao;
using Banking.Application.UseCases.Transacao.Depositar;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Domain.Seguranca.Transacoes;
using Banking.Exceptions;
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
        private readonly ILerCLienteRepository _clienteRepository;

        public DepositarUseCase(ITransacaoService transacaoService,
            IGravarDepositoRepository gravarDepositoRepository,
            IUnitOfWork unitOfWork,
            ISegurancaTransacao segurancaTransacao, IMapper mapper, ILerCLienteRepository clienteRepository)
        {
            _transacaoService = transacaoService;
            _gravarDepositoRepository = gravarDepositoRepository;
            _unitOfWork = unitOfWork;
            _segurancaTransacao = segurancaTransacao;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseDepositarJson> Execute(RequestExecutarTransacaoJson request)
        {
            await Validate(request);

            var cliente = await _clienteRepository.GetClienteByNumeroConta(request.numeroConta);

            if (cliente == null)
                throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

            var valorTransacao = double.Parse(request.valorTransacao);

            _transacaoService.ExecutarDeposito(cliente.Conta, valorTransacao);

            var deposito = new Domain.Entities.Deposito
            {
                CPFCliente = cliente.CPF,
                Nome = cliente.Nome,
                ContaDeposito = new Domain.Entities.AuxiliarTransacao
                {
                    numeroAgencia = cliente.Conta.NumeroAgencia,
                    numeroConta = cliente.Conta.NumeroConta,
                    numeroBanco = cliente.Conta.NumeroBanco
                },
                ValorDeposito = valorTransacao,
                NumeroDeposito = _segurancaTransacao.GerarNumeroTransacao()
            };

            await _gravarDepositoRepository.Add(deposito);

            cliente.Conta.AdicionarDeposito(deposito);

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