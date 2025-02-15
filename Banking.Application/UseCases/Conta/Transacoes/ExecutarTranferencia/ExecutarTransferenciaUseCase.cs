using AutoMapper;
using Banking.Application.Services.Transacao;
using Banking.Application.UseCases.Transacao.ExecutarTranferencia;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Entities;
using Banking.Domain.Repositories;
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

        public ExecutarTransferenciaUseCase(ILoggedCliente loggedCliente, ITransacaoService transacaoService,
            ISegurancaTransacao segurancaTransacao, IGravarTransferenciaRepository gravarTransferenciaRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _loggedCliente = loggedCliente;
            _transacaoService = transacaoService;
            _segurancaTransacao = segurancaTransacao;
            _gravarTransferenciaRepository = gravarTransferenciaRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseExecutarTransferenciaJson> Execute(RequestExecutarTransacaoJson request)
        {
            if (!double.TryParse(request.valorTransacao, out double valor) || valor <= 0)
                throw new BusinessException("O valor da transação é inválido.");

            var clienteAutenticado = await _loggedCliente.GetClienteByToken();

            if (clienteAutenticado == null)
                throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

            var contaOrigem =
                await _transacaoService.ObterConta(clienteAutenticado.UserIdentifier, clienteAutenticado.NumeroConta);


            var clienteDestino = await _transacaoService.ObterClienteByNumeroConta(request.numeroConta);
            var contaDestino =
                await _transacaoService.ObterConta(request.numeroConta, request.numeroBanco, request.numeroAgencia);

            await _transacaoService.ExecutarTransferencia(contaOrigem, contaDestino, valor);

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
                    numeroAgencia = contaDestino.NumeroAgencia,
                    numeroBanco = contaDestino.NumeroBanco,
                    numeroConta = contaDestino.NumeroConta,
                },
                NomeClienteDestino = clienteDestino.Nome,
                NomeClienteOrigem = clienteAutenticado.Nome,
                ValorTransacao = valor,
                NumeroTransacao = numeroTransacao,
                CpfClienteDestino = clienteDestino.CPF,
                CpfClienteOrigem = clienteAutenticado.CPF
            };
            contaDestino.AdicionarTransferencia(transferencia);

            await _gravarTransferenciaRepository.Add(transferencia);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseExecutarTransferenciaJson>(transferencia);
        }
    }
}