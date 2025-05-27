using System.Transactions;
using AutoMapper;
using Banking.Communication.Requests.Conta.Transferencia;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Transferencia;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetTransferenciaByNumero;

public class GetTransferenciaByNumeroUseCase : IGetTransferenciaByNumeroUseCase
{
    private readonly ILerTransferenciaRepository _lerTransferenciaRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedCliente _loggedCliente;

    public GetTransferenciaByNumeroUseCase(ILerTransferenciaRepository lerTransferenciaRepository, IMapper mapper,
        ILoggedCliente loggedCliente)
    {
        _lerTransferenciaRepository = lerTransferenciaRepository;
        _mapper = mapper;
        _loggedCliente = loggedCliente;
    }

    public async Task<ResponseExecutarTransferenciaJson> Execute(RequestGetTransferenciaByNumeroJson request)
    {
        await TransferenciaValidator(request);

        var cliente = await _loggedCliente.GetClienteByToken();
        if (cliente is null)
            throw new TransferenciaException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        var transferencia =
            await _lerTransferenciaRepository.GetTransferenciaByNumeroAsync(request.NumeroTransferencia, cliente.Id);

        if (transferencia is null)
            throw new TransferenciaException(ResourceMessagesExceptions.TRANSFERENCIA_NAO_ENCONTRADA);

        return _mapper.Map<ResponseExecutarTransferenciaJson>(transferencia);
    }

    private async Task TransferenciaValidator(RequestGetTransferenciaByNumeroJson request)
    {
        var validator = new GetTransferenciaByNumeroValidator();

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new ErrorsOnValidateExceptions(validationResult.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}