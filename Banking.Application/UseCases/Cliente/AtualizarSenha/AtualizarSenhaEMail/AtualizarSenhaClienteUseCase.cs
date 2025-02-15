using Banking.Application.Services.Encryption;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaEMail;

public class AtualizarSenhaClienteUseCase : IAtualizarSenhaClienteUseCase
{
    private readonly ILerCLienteRepository _lerCLienteRepository;
    private readonly IGravarClienteRepository _gravarClienteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordEncryptor _passwordEncryptor;

    public AtualizarSenhaClienteUseCase(ILerCLienteRepository lerCLienteRepository,
        IGravarClienteRepository gravarClienteRepository, IUnitOfWork unitOfWork, PasswordEncryptor passwordEncryptor)
    {
        _lerCLienteRepository = lerCLienteRepository;
        _gravarClienteRepository = gravarClienteRepository;
        _unitOfWork = unitOfWork;
        _passwordEncryptor = passwordEncryptor;
    }

    public async Task<ResponseAtualizarClienteJson> Execute(RequestAtualizarSenhaClienteJson request)
    {
        await AtualizarSenhaValidator(request);

        var cliente = await _lerCLienteRepository.GetClienteByEmail(request.Email);

        if (cliente == null)
            throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

        SenhaValidation(request, cliente);

        cliente.Senha = _passwordEncryptor.Encript(request.NovaSenha);

        _gravarClienteRepository.AtualizarSenhaCliente(cliente);

        await _unitOfWork.Commit();


        return new ResponseAtualizarClienteJson()
        {
            Mensagem = "Senha atualizada com sucesso",
            Sucesso = true,
            DataDeAtualizacao = DateTime.UtcNow
        };
    }

    private async Task AtualizarSenhaValidator(RequestAtualizarSenhaClienteJson request)
    {
        var validator = new AtualizarSenhaValidator();

        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            throw new ErrorsOnValidateExceptions(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }

    private void SenhaValidation(RequestAtualizarSenhaClienteJson request, Domain.Entities.Cliente cliente)
    {
        if (!_passwordEncryptor.Verify(request.SenhaAtual, cliente.Senha))
        {
            throw new BusinessException(ResourceMessagesExceptions.SENHA_INCORRETA);
        }

        var novaSenhaEncriptada = _passwordEncryptor.Encript(request.NovaSenha);

        if (novaSenhaEncriptada == cliente.Senha)
        {
            throw new BusinessException(ResourceMessagesExceptions.SENHA_IGUAL);
        }

        if (request.NovaSenha != request.ConfirmarNovaSenha)
        {
            throw new BusinessException(ResourceMessagesExceptions.SENHAS_DEVEM_COINCIDIR);
        }
    }
}