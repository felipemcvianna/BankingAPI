using Banking.Application.Services.Encryption;
using Banking.Communication.Requests.Login;
using Banking.Communication.Response.Acesso.Login;
using Banking.Domain.Repositories.Cliente;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Acesso.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly ILerCLienteRepository _lerCLienteRepository;
        private readonly PasswordEncryptor _passwordEncryptor;

        public LoginUseCase(ILerCLienteRepository lerCLienteRepository, PasswordEncryptor passwordEncryptor)
        {
            _lerCLienteRepository = lerCLienteRepository;
            _passwordEncryptor = passwordEncryptor;
        }

        public async Task<ResponseLoginJson> Execute(RequestLoginJson request)
        {
            Validator(request);

            var clienteLogin = await _lerCLienteRepository.GetClienteByEmail(request.Email);

            if (clienteLogin == null)
                throw new BusinessException("CLIENTE NÃO ENCONTRADO");

            if (!ValidatePassword(request.Senha, clienteLogin.Senha))
                throw new BusinessException("SENHA INCORRETA");


            return new ResponseLoginJson()
            {
                Nome = clienteLogin.Nome
            };

        }
        private void Validator(RequestLoginJson request)
        {
            var loginValidator = new LoginValidator();

            var result = loginValidator.Validate(request);

            if (!result.IsValid)
            {
                var errorsMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new BusinessException(errorsMessages);
            }
        }

        private bool ValidatePassword(string senhaRequest, string senhaRegistrada)
        {
            return _passwordEncryptor.Verify(senhaRequest, senhaRegistrada);
        }
    }
}
