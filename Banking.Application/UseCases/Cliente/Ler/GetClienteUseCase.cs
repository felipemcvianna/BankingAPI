using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories.Cliente;
using Banking.Exceptions.ExceptionBase;
using FluentValidation.Results;

namespace Banking.Application.UseCases.Cliente.Ler;

public class GetClienteUseCase : IGetClienteUseCase
{
    private readonly ILerCLienteRepository _lerClienteRepository;

    public GetClienteUseCase(ILerCLienteRepository lerClienteRepository)
    {
        _lerClienteRepository = lerClienteRepository;
    }

    public async Task<ResponseGetClienteJson> Execute(RequestGetCliente request)
    {
        await Validate(request);

        var cliente = await _lerClienteRepository.GetClienteByEmail(request.Email);

        return new ResponseGetClienteJson
        {
            Nome = cliente.Nome,
            NumeroConta = "***" + cliente.NumeroConta
        };
    }

    private async Task Validate(RequestGetCliente request)
    {
        var validate = new GetClienteValidator();
        var result = await validate.ValidateAsync(request);

        var emailExiste = await _lerClienteRepository.ExisteClienteComEmail(null, request.Email);

        if (!emailExiste)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "EMAIL NÃO PERTENCE A NENHUM CLIENTE"));
        }

        if (!result.IsValid)
        {
            var errorsList = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorsOnValidateExceptions(errorsList);
        }
    }
}