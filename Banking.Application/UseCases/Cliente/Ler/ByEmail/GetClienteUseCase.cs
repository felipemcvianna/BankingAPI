using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories.Cliente;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;
using FluentValidation.Results;

namespace Banking.Application.UseCases.Cliente.Ler.ByEmail;

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

        var ultimoNumero = cliente!.NumeroConta.ToString();

        var numeroSeguro = ultimoNumero.Substring(ultimoNumero.Length - 1, 1);

        return new ResponseGetClienteJson
        {
            Nome = cliente.Nome,
            NumeroConta = "***" + numeroSeguro
        };
    }

    private async Task Validate(RequestGetCliente request)
    {
        var validate = new GetClienteValidator();
        var result = await validate.ValidateAsync(request);

        var emailExiste = await _lerClienteRepository.ExisteClienteComEmail(request.Email);

        if (!emailExiste)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesExceptions.EMAIL_NAO_CADASTRADO));
        }

        if (!result.IsValid)
        {
            var errorsList = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorsOnValidateExceptions(errorsList);
        }
    }
}