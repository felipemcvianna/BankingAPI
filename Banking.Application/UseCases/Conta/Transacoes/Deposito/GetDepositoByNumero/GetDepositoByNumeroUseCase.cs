using AutoMapper;
using Banking.Communication.Requests.Conta.Deposito;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByNumero;

public class GetDepositoByNumeroUseCase : IGetDepositoByNumeroUseCase
{
    private readonly ILerDepositosRepository _lerDepositosRepository;
    private readonly IMapper _mapper;

    public GetDepositoByNumeroUseCase(IMapper mapper, ILerDepositosRepository lerDepositosRepository)
    {
        _mapper = mapper;
        _lerDepositosRepository = lerDepositosRepository;
    }

    public async Task<ResponseDepositarJson> Execute(RequestGetDepositoByNumeroJson request)
    {
        var deposito = await _lerDepositosRepository.ObterDepositoPorNumero(request.NumeroDeposito);


        if (deposito == null)
        {
            throw new DepositoException(ResourceMessagesExceptions.DEPOSITO_NAO_ENCONTRADO);
        }

        var response = _mapper.Map<ResponseDepositarJson>(deposito);

        return response;
    }
}