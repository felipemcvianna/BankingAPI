using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
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
            throw new BusinessException("EMAIL NÃO PERTENCE A NENHUM CLIENTE.");
        }
    }

    private void SenhaValidation(string senhaAtual, RequestAtualizarSenhaClienteJson request)
    {
        if (senhaAtual != request.SenhaAtual)
        {
            throw new BusinessException("CONFIRME A SENHA ATUAL");
        }

        if (request.NovaSenha == senhaAtual)
        {
            throw new BusinessException("A SENHA NÃO PODE SER IGUAL A SENHA ATUAL.");
        }

        if (request.NovaSenha != request.ConfirmarNovaSenha)
        {
            throw new BusinessException("AS SENHAS DEVEM SER IGUAIS.");
        }
    }
}