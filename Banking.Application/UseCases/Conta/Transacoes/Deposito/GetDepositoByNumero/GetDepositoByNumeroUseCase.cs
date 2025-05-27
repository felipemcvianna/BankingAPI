using AutoMapper;
using Banking.Communication.Requests.Conta.Deposito;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByNumero;

public class GetDepositoByNumeroUseCase : IGetDepositoByNumeroUseCase
{
    private readonly ILerDepositosRepository _lerDepositosRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedCliente _loggedCliente;

    public GetDepositoByNumeroUseCase(IMapper mapper, ILerDepositosRepository lerDepositosRepository,
        ILoggedCliente loggedCliente)
    {
        _mapper = mapper;
        _lerDepositosRepository = lerDepositosRepository;
        _loggedCliente = loggedCliente;
    }

    public async Task<ResponseDepositarJson> Execute(RequestGetDepositoByNumeroJson request)
    {
        var cliente = await _loggedCliente.GetClienteByToken();
        if (cliente == null)
            throw new DataDepositoException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        var deposito = await _lerDepositosRepository.ObterDepositoPorNumero(request.NumeroDeposito, cliente.Id);

        if (deposito == null)
        {
            throw new DepositoException(ResourceMessagesExceptions.DEPOSITO_NAO_ENCONTRADO);
        }

        var response = _mapper.Map<ResponseDepositarJson>(deposito);

        return response;
    }
}