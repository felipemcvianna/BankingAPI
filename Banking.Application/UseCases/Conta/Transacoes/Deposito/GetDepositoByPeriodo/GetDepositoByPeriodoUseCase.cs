using System.Globalization;
using AutoMapper;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Deposito;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByPeriodo;

public class GetDepositoByPeriodoUseCase : IGetDepositoByPeriodoUseCase
{
    private readonly ILerDepositosRepository _lerDepositosRepository;
    private readonly IMapper _mapper;

    public GetDepositoByPeriodoUseCase(ILerDepositosRepository lerDepositosRepository, IMapper mapper)
    {
        _lerDepositosRepository = lerDepositosRepository;
        _mapper = mapper;
    }

    public async Task<List<ResponseDepositarJson>> Execute(RequestGetDepositoByPeriodo request)
    {
        var startDate =
            DateTime.SpecifyKind(DateTime.ParseExact(request.DataInicial, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DateTimeKind.Utc);

        var endDate =
            DateTime.SpecifyKind(DateTime.ParseExact(request.DataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DateTimeKind.Utc);

        var depositos = await _lerDepositosRepository.GetDepositosByPeriodo(startDate, endDate);

        var response = _mapper.Map<List<ResponseDepositarJson>>(depositos);

        return response;
    }
}