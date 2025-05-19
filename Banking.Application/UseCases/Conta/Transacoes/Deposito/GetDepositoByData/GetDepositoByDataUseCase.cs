using System.Globalization;
using AutoMapper;
using Banking.Communication.Requests.Conta.Deposito;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;
using Microsoft.Extensions.Logging;
using static System.Globalization.CultureInfo;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByData;

public class GetDepositoByDataUseCase : IGetDepositoByDataUseCase
{
    private readonly ILerDepositosRepository _depositosRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedCliente _loggedCliente;

    public GetDepositoByDataUseCase(ILerDepositosRepository depositosRepository, IMapper mapper,
        ILoggedCliente loggedCliente)
    {
        _depositosRepository = depositosRepository;
        _mapper = mapper;
        _loggedCliente = loggedCliente;
    }

    public async Task<List<ResponseDepositarJson>> Execute(RequestGetDepositoByDataJson request)
    {
        var cliente = await _loggedCliente.GetClienteByToken();

        if (cliente == null)
            throw new DataDepositoException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);


        if (string.IsNullOrEmpty(request.DataDeposito))
            throw new DataDepositoException(ResourceMessagesExceptions.DATA_VAZIA);

        if (!DateTime.TryParseExact(request.DataDeposito, "dd/MM/yyyy", InvariantCulture,
                DateTimeStyles.None, out var dataDeposito))
        {
            throw new DataDepositoException(@ResourceMessagesExceptions.DATA_FORMATO_INVALIDO);
        }

        dataDeposito = DateTime.SpecifyKind(dataDeposito, DateTimeKind.Utc);

        if (dataDeposito.Date > DateTime.Now.Date)
            throw new DataDepositoException(ResourceMessagesExceptions.DATA_NO_FUTURO);

        var listaDepositos = await _depositosRepository.ObterDepositoByData(dataDeposito, cliente.Id);
        return _mapper.Map<List<ResponseDepositarJson>>(listaDepositos);
    }
}