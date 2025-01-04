using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Cliente.AtualizarSenha;

public class AtualizarSenhaClienteUseCase : IAtualizarSenhaClienteUseCase
{
    private readonly ILerCLienteRepository _lerCLienteRepository;
    private readonly IGravarClienteRepository _gravarClienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AtualizarSenhaClienteUseCase(ILerCLienteRepository lerCLienteRepository,
        IGravarClienteRepository gravarClienteRepository, IUnitOfWork unitOfWork)
    {
        _lerCLienteRepository = lerCLienteRepository;
        _gravarClienteRepository = gravarClienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseAtualizarClienteJson> Execute(RequestAtualizarSenhaClienteJson request)
    {
        await AtualizarSenhaValidator(request);

        var cliente = await _lerCLienteRepository.GetClienteByEmail(request.Email);

        EmailESenhaValidation(cliente, request);
        SenhaValidation(cliente!.Senha, request);

        cliente!.Senha = request.NovaSenha;

        _gravarClienteRepository.AtualizarSenhaCliente(cliente);

        await _unitOfWork.Commit();


        return new ResponseAtualizarClienteJson()
        {
            Mensagem = "SENHA ATUALIZADA",
            Sucesso = true,
            DataDeAtualizacao = DateTime.Now
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

    private void EmailESenhaValidation(Domain.Entities.Cliente? cliente, RequestAtualizarSenhaClienteJson request)
    {
        if (cliente == null)
        {
            throw new BusinessException(ResourceMessagesExceptions.EMAIL_NAO_CADASTRADO);
        }
    }

    private void SenhaValidation(string senhaAtual, RequestAtualizarSenhaClienteJson request)
    {
        if (senhaAtual != request.SenhaAtual)
        {
            throw new BusinessException(ResourceMessagesExceptions.SENHA_ATUAL);
        }

        if (request.NovaSenha == senhaAtual)
        {
            throw new BusinessException(ResourceMessagesExceptions.SENHA_IGUAL);
        }

        if (request.NovaSenha != request.ConfirmarNovaSenha)
        {
            throw new BusinessException(ResourceMessagesExceptions.SENHAS_DEVER_COINCIDIR);
        }
    }
}