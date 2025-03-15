using System.Globalization;
using AutoMapper;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase.Deposito;
using static System.Globalization.CultureInfo;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByData;

public class GetDepositoByDataUseCase : IGetDepositoByDataUseCase
{
    private readonly ILerDepositosRepository _depositosRepository;
    private readonly IMapper _mapper;

    public GetDepositoByDataUseCase(ILerDepositosRepository depositosRepository, IMapper mapper)
    {
        _depositosRepository = depositosRepository;
        _mapper = mapper;
    }

    public async Task<List<ResponseDepositarJson>> Execute(RequestGetDepositoByData request)
    {
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

        var listaDepositos = await _depositosRepository.ObterDepositoByData(dataDeposito);
        return _mapper.Map<List<ResponseDepositarJson>>(listaDepositos);
    }
}