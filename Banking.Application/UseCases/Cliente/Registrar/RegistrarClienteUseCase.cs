using AutoMapper;
using Banking.Application.Services.Encryption;
using Banking.Application.UseCases.Conta;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Exceptions.ExceptionBase;
using FluentValidation.Results;

namespace Banking.Application.UseCases.Cliente.Registrar;

public class RegistrarClienteUseCase : IRegistrarClienteUseCase
{
    private readonly IMapper _mapper;
    private readonly IGravarClienteRepository _gravarClienteRepository;
    private readonly ILerCLienteRepository _lerCLienteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRegistrarContaUseCase _registrarContaUseCase;
    private readonly PasswordEncryptor _passwordEncryptor;


    public RegistrarClienteUseCase(IMapper mapper,
        IGravarClienteRepository gravarClienteRepository,
        ILerCLienteRepository lerCLienteRepository, IUnitOfWork unitOfWork,
        IRegistrarContaUseCase contaUseCase, PasswordEncryptor passwordEncryptor)
    {
        _mapper = mapper;
        _gravarClienteRepository = gravarClienteRepository;
        _lerCLienteRepository = lerCLienteRepository;
        _unitOfWork = unitOfWork;
        _registrarContaUseCase = contaUseCase;
        _passwordEncryptor = passwordEncryptor;
    }


    public async Task<ResponseRegistrarClienteJson> Execute(RequestRegistrarClienteJson request)
    {
        await Validator(request);
        var cliente = _mapper.Map<Domain.Entities.Cliente>(request);
        cliente.UserIdentifier = Guid.NewGuid();
        cliente.Senha = _passwordEncryptor.Encript(request.Senha);

        var numeroConta = await _registrarContaUseCase.Execute();
        cliente.NumeroConta = numeroConta.NumeroConta;

        await _gravarClienteRepository.Add(cliente);
        await _unitOfWork.Commit();

        var response = _mapper.Map<ResponseRegistrarClienteJson>(cliente);

        return response;
    }

    private async Task Validator(RequestRegistrarClienteJson request)
    {
        var validator = new RegistrarClienteValidator();

        var result = await validator.ValidateAsync(request);

        var existeCpf = await _lerCLienteRepository.ExisteClienteComCpf(request.CPF);

        if (existeCpf)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, "CPF JÁ CADASTRADO"));
        }

        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorsOnValidateExceptions(errorsMessages);
        }
    }
}