using System.Reflection;
using System.Transactions;
using AutoMapper;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Transferencia;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetAllTransferencias;

public class GetAllTransferenciasUseCaseUseCase : IGetAllTransferenciasUseCase
{
    private readonly ILerTransferenciaRepository _lerTransferenciaRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedCliente _loggedCliente;

    public GetAllTransferenciasUseCaseUseCase(IMapper mapper, ILerTransferenciaRepository lerTransferenciaRepository,
        ILoggedCliente loggedCliente)
    {
        _mapper = mapper;
        _lerTransferenciaRepository = lerTransferenciaRepository;
        _loggedCliente = loggedCliente;
    }

    public async Task<List<ResponseExecutarTransferenciaJson>> Execute()
    {
        var cliente = await _loggedCliente.GetClienteByToken();

        if (cliente == null)
            throw new TransferenciaException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        var listaTransferencias = await _lerTransferenciaRepository.GetAllTransferenciasAsync(cliente.CPF);

        return _mapper.Map<List<ResponseExecutarTransferenciaJson>>(listaTransferencias);
    }
}