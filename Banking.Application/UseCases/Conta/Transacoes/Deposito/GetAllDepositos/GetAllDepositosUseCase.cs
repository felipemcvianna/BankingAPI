using AutoMapper;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetAllDepositos;

public class GetAllDepositosUseCase : IGetAllDepositosUseCase
{
    private readonly ILerDepositosRepository _lerDepositosRepository;
    private readonly ILoggedCliente _loggedCliente;
    private readonly IMapper _mapper;

    public GetAllDepositosUseCase(ILerDepositosRepository lerDepositosRepository, ILoggedCliente loggedCliente,
        IMapper mapper)
    {
        this._lerDepositosRepository = lerDepositosRepository;
        _loggedCliente = loggedCliente;
        _mapper = mapper;
    }

    public async Task<List<ResponseDepositarJson>> Execute()
    {
        var cliente = await _loggedCliente.GetClienteByToken();

        if (cliente is null)
            throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        var depositos = await _lerDepositosRepository.GetAllDepositos(cliente.CPF);

        return _mapper.Map<List<ResponseDepositarJson>>(depositos);
    }
}