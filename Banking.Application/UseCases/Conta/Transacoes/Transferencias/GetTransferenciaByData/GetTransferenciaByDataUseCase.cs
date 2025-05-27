using System.Globalization;
using AutoMapper;
using Banking.Communication.Requests.Conta.Transferencia;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Transferencia;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetTransferenciaByData;

public class GetTransferenciaByDataUseCase : IGetTransferenciaByDataUseCase
{
    private readonly ILerTransferenciaRepository _lerTransferenciaRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedCliente _loggedCliente;

    public GetTransferenciaByDataUseCase(ILerTransferenciaRepository lerTransferenciaRepository
        , IMapper mapper, ILoggedCliente loggedCliente)
    {
        _lerTransferenciaRepository = lerTransferenciaRepository;
        _mapper = mapper;
        _loggedCliente = loggedCliente;
    }

    public async Task<List<ResponseExecutarTransferenciaJson>> Execute(RequestGetTransferenciaByData request)
    {
        await Validate(request);

        var cliente = await _loggedCliente.GetClienteByToken();

        if (cliente is null)
            throw new TransferenciaException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        if (string.IsNullOrEmpty(request.DataTrasferencia))
            throw new TransferenciaException(ResourceMessagesExceptions.DATA_VAZIA);

        if (!DateTime.TryParseExact(request.DataTrasferencia, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dataTransferencia))
        {
            throw new TransferenciaException(@ResourceMessagesExceptions.DATA_FORMATO_INVALIDO);
        }

        dataTransferencia = DateTime.SpecifyKind(dataTransferencia, DateTimeKind.Utc);

        if (dataTransferencia.Date > DateTime.Now.Date)
            throw new DataDepositoException(ResourceMessagesExceptions.DATA_NO_FUTURO);

        var transferencia =
            await _lerTransferenciaRepository.GetTransferenciaByDataAsync(dataTransferencia.Date, cliente.Id);

        return _mapper.Map<List<ResponseExecutarTransferenciaJson>>(transferencia);
    }

    private async Task Validate(RequestGetTransferenciaByData request)
    {
        var validator = new GetTransferenciaByDateValidator();

        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            throw new TransferenciaException(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}