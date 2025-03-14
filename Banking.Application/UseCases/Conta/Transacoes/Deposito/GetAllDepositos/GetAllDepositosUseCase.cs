using AutoMapper;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Domain.Repositories.Transacoes.Deposito;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Deposito.GetAllDepositos;

public class GetAllDepositosUseCase : IGetAllDepositosUseCase
{
    private readonly ILoggedCliente _loggedCliente;
    private readonly ILerDepositosRepository _lerDepositosRepository;
    private readonly IMapper _mapper;

    public GetAllDepositosUseCase(ILoggedCliente loggedCliente, ILerDepositosRepository lerDepositosRepository,
        IMapper mapper)
    {
        _loggedCliente = loggedCliente;
        _lerDepositosRepository = lerDepositosRepository;
        _mapper = mapper;
    }

    public async Task<List<ResponseDepositarJson>> Execute()
    {
        var cliente = await _loggedCliente.GetClienteByToken();

        if (cliente == null)
            throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        var listaDepositos = await _lerDepositosRepository.GetAllDepositos(cliente.CPF);

        var response = _mapper.Map<List<ResponseDepositarJson>>(listaDepositos);

        return response;
    }
}