using AutoMapper;
using Banking.Application.Services.Transacao;
using Banking.Application.UseCases.Transacao.ExecutarTranferencia;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Entities;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Repositories.Transacoes.Transferencia;
using Banking.Domain.Seguranca.Tokens;
using Banking.Domain.Seguranca.Transacoes;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.ExecutarTranferencia
{
    public class ExecutarTransferenciaUseCase : IExecutarTransferenciaUseCase
    {
        private readonly ILoggedCliente _loggedCliente;
        private readonly ITransacaoService _transacaoService;
        private readonly ISegurancaTransacao _segurancaTransacao;
        private readonly IGravarTransferenciaRepository _gravarTransferenciaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILerCLienteRepository _clienteRepository;

        public ExecutarTransferenciaUseCase(ILoggedCliente loggedCliente, ITransacaoService transacaoService,
            ISegurancaTransacao segurancaTransacao, IGravarTransferenciaRepository gravarTransferenciaRepository,
            IUnitOfWork unitOfWork, IMapper mapper, ILerCLienteRepository clienteRepository)
        {
            _loggedCliente = loggedCliente;
            _transacaoService = transacaoService;
            _segurancaTransacao = segurancaTransacao;
            _gravarTransferenciaRepository = gravarTransferenciaRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseExecutarTransferenciaJson> Execute(RequestExecutarTransacaoJson request)
        {
            await TransferenciaValidator(request);

            var valor = double.Parse(request.valorTransacao);

            var clienteAutenticado = await _loggedCliente.GetClienteByToken() ??
                                     throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

            var contaOrigem =
                await _transacaoService.ObterConta(clienteAutenticado.UserIdentifier, clienteAutenticado.NumeroConta);


            var contaClienteDestino = await _clienteRepository.GetClienteAndConta(request.numeroConta);

            await _transacaoService.ExecutarTransferencia(contaOrigem, contaClienteDestino.Conta, valor);

            var numeroTransacao = _segurancaTransacao.GerarNumeroTransacao();

            var transferencia = new Transferencia()
            {
                ContaOrigem = new AuxiliarTransacao()
                {
                    numeroAgencia = contaOrigem.NumeroAgencia,
                    numeroBanco = contaOrigem.NumeroBanco,
                    numeroConta = contaOrigem.NumeroConta,
                },
                ContaDestino = new AuxiliarTransacao()
                {
                    numeroAgencia = contaClienteDestino.Conta.NumeroAgencia,
                    numeroBanco = contaClienteDestino.Conta.NumeroBanco,
                    numeroConta = contaClienteDestino.Conta.NumeroConta,
                },
                NomeClienteDestino = contaClienteDestino.Nome,
                NomeClienteOrigem = clienteAutenticado.Nome,
                ValorTransacao = valor,
                NumeroTransacao = numeroTransacao,
                CpfClienteDestino = contaClienteDestino.CPF,
                CpfClienteOrigem = clienteAutenticado.CPF
            };
            contaClienteDestino.Conta.AdicionarTransferencia(transferencia);

            await _gravarTransferenciaRepository.Add(transferencia);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseExecutarTransferenciaJson>(transferencia);
        }

        private async Task TransferenciaValidator(RequestExecutarTransacaoJson request)
        {
            var validator = new ExecutarTransferenciaValidator();

            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ErrorsOnValidateExceptions(result.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }
    }
}