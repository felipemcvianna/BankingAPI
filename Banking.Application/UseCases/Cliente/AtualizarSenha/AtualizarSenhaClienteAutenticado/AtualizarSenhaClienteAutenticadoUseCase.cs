using Banking.Application.Services.Encryption;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;
using FluentValidation.Results;

namespace Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaClienteAutenticado
{
    public class AtualizarSenhaClienteAutenticadoUseCase : IAtualizarSenhaClienteAutenticadoUseCase
    {
        private readonly ILoggedCliente _loggedCliente;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly IGravarClienteRepository _gravarClienteRepository;

        public AtualizarSenhaClienteAutenticadoUseCase(ILoggedCliente loggedCliente,
            IUnitOfWork unitOfWork, PasswordEncryptor passwordEncryptor,
            IGravarClienteRepository gravarClienteRepository)
        {
            _loggedCliente = loggedCliente;
            _unitOfWork = unitOfWork;
            _passwordEncryptor = passwordEncryptor;
            _gravarClienteRepository = gravarClienteRepository;
        }

        public async Task<ResponseAtualizarClienteJson> Execute(RequestAtualizarSenhaClienteAutenticadoJson request)
        {
            await Validator(request);

            var cliente = await _loggedCliente.GetClienteByToken();

            if (cliente == null)
                throw new BusinessException(ResourceMessagesExceptions.CLIENTE_NAO_ENCONTRADO);

            if (!_passwordEncryptor.Verify(request.senhaAtual, cliente.Senha))
                throw new ErrorsOnValidateExceptions(ResourceMessagesExceptions.SENHA_INCORRETA);

            cliente.Senha = _passwordEncryptor.Encript(request.novaSenha);

            _gravarClienteRepository.AtualizarSenhaCliente(cliente);

            await _unitOfWork.Commit();

            return new ResponseAtualizarClienteJson()
            {
                Mensagem = "Senha atualizada com sucesso",
                Sucesso = true,
                DataDeAtualizacao = DateTime.UtcNow
            };
        }

        private async Task Validator(RequestAtualizarSenhaClienteAutenticadoJson request)
        {
            var validator = new AtualizarSenhaClienteAutenticadoValidator();

            var result = await validator.ValidateAsync(request);
            
            if (!result.IsValid)
                throw new ErrorsOnValidateExceptions(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}