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

    public DeletarClienteUseCase(ILerCLienteRepository lerCLienteRepository,
        IDeletarClienteRepository deletarClienteRepository, IUnitOfWork unitOfWork)
    {
        _lerCLienteRepository = lerCLienteRepository;
        _deletarClienteRepository = deletarClienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDeletarClienteJson> Execute(RequestDeletarClienteJson request)
    {
        await Validator(request);

        var cliente = await _lerCLienteRepository.GetClienteByEmail(request.Email);

        EmailESenhaValidation(cliente, request);

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

    private void EmailESenhaValidation(Domain.Entities.Cliente cliente, RequestDeletarClienteJson request)
    {
        if (cliente == null)
        {
            throw new BusinessException("EMAIL NÃO PERTENCE A NENHUM CLIENTE");
        }

        if (cliente.Senha != request.Senha)
        {
            throw new BusinessException("A SENHA ESTÁ INCORRETA");
        }
    }
>>>>>>> 3c5e9f2 (Caso de uso deletar cliente)
}