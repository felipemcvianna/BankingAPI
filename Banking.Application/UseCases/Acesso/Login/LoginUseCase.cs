using Banking.Application.Services.Encryption;
using Banking.Communication.Requests.Login;
using Banking.Communication.Response.Login;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

namespace Banking.Application.UseCases.Acesso.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly ILerCLienteRepository _lerCLienteRepository;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly IAcessTokenGenerator _acessTokenGenerator;

        public LoginUseCase(ILerCLienteRepository lerCLienteRepository,
            IAcessTokenGenerator tokenGenerator,
            PasswordEncryptor passwordEncryptor)
        {
            _lerCLienteRepository = lerCLienteRepository;
            _acessTokenGenerator = tokenGenerator;
            _passwordEncryptor = passwordEncryptor;
        }

        public async Task<ResponseLoginJson> Execute(RequestLoginJson request)
        {
            Validator(request);

            var clienteLogin = await _lerCLienteRepository.GetClienteByEmail(request.Email);

            if (clienteLogin == null)
                throw new BusinessException(ResourceMessagesExceptions.EMAIL_NAO_CADASTRADO);

            if (!ValidatePassword(request.Senha, clienteLogin.Senha))
                throw new BusinessException(ResourceMessagesExceptions.SENHA_INCORRETA);


            return new ResponseLoginJson()
            {
                Nome = clienteLogin.Nome,
                Tokens = new Communication.Token.ResponseTokensJson
                {
                    AcessToken = _acessTokenGenerator.GenerateToken(clienteLogin.UserIdentifier)
                }
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
