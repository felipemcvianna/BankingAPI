using Banking.Application.Services.Encryption;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Cliente.Deletar;

public class DeletarClienteUseCase : IDeletarClienteUseCase
{
    private readonly ILerCLienteRepository _lerCLienteRepository;
    private readonly IDeletarClienteRepository _deletarClienteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordEncryptor _passwordEncryptor;

    public DeletarClienteUseCase(ILerCLienteRepository lerCLienteRepository,
        IDeletarClienteRepository deletarClienteRepository, IUnitOfWork unitOfWork, PasswordEncryptor passwordEncryptor)
    {
        _lerCLienteRepository = lerCLienteRepository;
        _deletarClienteRepository = deletarClienteRepository;
        _unitOfWork = unitOfWork;
        _passwordEncryptor = passwordEncryptor;
    }

    public async Task<ResponseDeletarClienteJson> Execute(RequestDeletarClienteJson request)
    {
        await Validator(request);

        var cliente = await _lerCLienteRepository.GetClienteByEmail(request.Email);

        if (cliente == null)
            throw new BusinessException("ESSE EMAIL NÃO PERTENCE A NENHUM CLIENTE");

        if (!PasswordVerify(request.Senha, cliente.Senha))
            throw new BusinessException("SENHA INCORRETA");

        var response = new ResponseDeletarClienteJson()
        {
            Email = cliente.Email,
            Nome = cliente.Nome
        };

        _deletarClienteRepository.Deletar(cliente);

        await _unitOfWork.Commit();

        return response;

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

    private bool PasswordVerify(string requestSenha, string clienteSenha)
    {
        return _passwordEncryptor.Verify(requestSenha, clienteSenha);
    }
}