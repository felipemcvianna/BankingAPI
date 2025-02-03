using Banking.Application.Services.Encryption;
using Banking.Application.UseCases.Conta.Deletar;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Cliente.Deletar;

public class DeletarClienteUseCase : IDeletarClienteUseCase
{
    private ILoggedCliente _loggedCliente;
    private readonly IDeletarClienteRepository _deletarClienteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordEncryptor _passwordEncryptor;
    private readonly IDeletarContaUseCase _deletarContaUseCase;

    public DeletarClienteUseCase(IDeletarClienteRepository deletarClienteRepository,
        IUnitOfWork unitOfWork, PasswordEncryptor passwordEncryptor,
        IDeletarContaUseCase deletarContaUseCase, ILoggedCliente loggedCliente)
    {
        _deletarClienteRepository = deletarClienteRepository;
        _unitOfWork = unitOfWork;
        _passwordEncryptor = passwordEncryptor;
        _deletarContaUseCase = deletarContaUseCase;
        _loggedCliente = loggedCliente;
    }

    public async Task<ResponseDeletarClienteJson> Execute(RequestDeletarClienteJson request)
    {
        await Validator(request);

        var cliente = await _loggedCliente.GetClienteByToken();

        if (cliente == null)
            throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        VerifyPasswords(cliente, request);

        _deletarClienteRepository.Deletar(cliente);

        await _deletarContaUseCase.Execute(cliente.UserIdentifier, cliente.NumeroConta);

        await _unitOfWork.Commit();

        return new ResponseDeletarClienteJson()
        {
            Mensagem = $"{cliente.Nome}, conta removida com sucesso",
            Sucesso = true,

        };
    }

    private async Task Validator(RequestDeletarClienteJson request)
    {
        var validator = new DeletarClienteValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            throw new ErrorsOnValidateExceptions(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }

    public void VerifyPasswords(Domain.Entities.Cliente cliente, RequestDeletarClienteJson request)
    {

        if (!_passwordEncryptor.Verify(request.Senha, cliente.Senha))
            throw new BusinessException(ResourceMessagesExceptions.SENHA_INCORRETA);

        if(request.Senha != request.confirmarSenha)
            throw new BusinessException(ResourceMessagesExceptions.SENHAS_DEVEM_COINCIDIR);
    }
}