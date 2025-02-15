using Banking.Application.Services.Transacao;
using Banking.Domain.Entities;
using Banking.Domain.Repositories.Transacoes.Saque;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;
using Banking.Infrastructure.Seguranca.Tokens.GetCliente;

namespace Banking.Application.UseCases.Conta.Transacoes.Sacar.LerSaque.GetAllSaques;

public class GetAllSaquesUseCase : IGetAllSaquesUseCase
{
    private readonly ILoggedCliente _loggedCliente;
    private readonly ITransacaoService _transacaoService;
    private readonly ILerSaqueRepository _lerSaqueRepository;


    public GetAllSaquesUseCase(ILoggedCliente loggedCliente, ITransacaoService transacaoService,
        ILerSaqueRepository lerSaqueRepository)
    {
        _loggedCliente = loggedCliente;
        _transacaoService = transacaoService;
        _lerSaqueRepository = lerSaqueRepository;
    }

    public async Task<List<Saque>> Execute()
    {
        var cliente = await _loggedCliente.GetClienteByToken();
        if (cliente == null)
            throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        var conta = await _transacaoService.ObterConta(cliente.UserIdentifier, cliente.NumeroConta);

        var saques = await _lerSaqueRepository.LerTodosSaquesClienteAutenticado(conta.NumeroConta, conta.NumeroBanco,
            conta.NumeroAgencia);

        if (saques.Count == 0)
            throw new BusinessException("SAQUES VAZIOS");

        return saques;
    }
}