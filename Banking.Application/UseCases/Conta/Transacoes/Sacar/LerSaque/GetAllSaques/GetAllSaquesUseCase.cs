using Banking.Domain.Entities;
using Banking.Domain.Repositories.Transacoes.Saque;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Conta.Transacoes.Sacar.LerSaque.GetAllSaques;

public class GetAllSaquesUseCase : IGetAllSaquesUseCase
{
    private readonly ILoggedCliente _loggedCliente;
    private readonly ILerSaqueRepository _lerSaqueRepository;


    public GetAllSaquesUseCase(ILoggedCliente loggedCliente,
        ILerSaqueRepository lerSaqueRepository)
    {
        _loggedCliente = loggedCliente;
        _lerSaqueRepository = lerSaqueRepository;
    }

    public async Task<List<Saque>> Execute()
    {
        var cliente = await _loggedCliente.GetClienteAndContaByToken();
        if (cliente == null)
            throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        var saques = await _lerSaqueRepository.LerTodosSaquesClienteAutenticado(cliente.Conta.NumeroConta,
            cliente.Conta.NumeroBanco,
            cliente.Conta.NumeroAgencia);

        return saques;
    }
}