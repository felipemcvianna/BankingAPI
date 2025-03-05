﻿using Banking.Application.Services.Encryption;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Banking.Domain.Repositories;
using Banking.Domain.Repositories.Cliente;
using Banking.Domain.Seguranca.Tokens;
using Banking.Exceptions;
using Banking.Exceptions.ExceptionBase;

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

            cliente.Senha = SenhaValidation(request, cliente);

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

        private string SenhaValidation(RequestAtualizarSenhaClienteAutenticadoJson request,
            Domain.Entities.Cliente cliente)
        {
            if (!_passwordEncryptor.Verify(request.senhaAtual, cliente.Senha))
            {
                throw new BusinessException(ResourceMessagesExceptions.SENHA_INCORRETA);
            }

            var novaSenhaEncriptada = _passwordEncryptor.Encript(request.novaSenha);

            if (novaSenhaEncriptada == cliente.Senha)
            {
                throw new BusinessException(ResourceMessagesExceptions.SENHA_IGUAL);
            }

            if (request.novaSenha != request.confirmarSenha)
            {
                throw new BusinessException(ResourceMessagesExceptions.SENHAS_DEVEM_COINCIDIR);
            }

            return novaSenhaEncriptada;
        }
    }
}