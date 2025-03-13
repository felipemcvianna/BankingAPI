using Banking.Application.UseCases.Cliente.Registrar;
using Banking.Exceptions;
using CommomTestsUtilities;
using Shouldly;

namespace ValidatorsTests.ClienteValidatorsTests;

public class RegistrarClienteValidatorTests
{
    [Fact]
    public async Task RegistrarCliente_DadosValidos_DeveSerValido()
    {
        var validator = new RegistrarClienteValidator();

        var request = ClienteBuilder.RegistrarClienteBuild();

        var result = await validator.ValidateAsync(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public async Task RegistrarCliente_EmailVazio_DeveRetornarErrorDeValidacao()
    {
        var validator = new RegistrarClienteValidator();

        var request = ClienteBuilder.RegistrarClienteBuild();

        request.Email = string.Empty;

        var result = await validator.ValidateAsync(request);

        request.Email.ShouldBeEmpty();
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e =>
            e.PropertyName == "Email" &&
            e.ErrorMessage == ResourceMessagesExceptions.EMAIL_VAZIO
        );
    }

    [Fact]
    public async Task RegistrarCliente_EmailInvalido_DeveRetornarErrorDeValidacao()
    {
        var validator = new RegistrarClienteValidator();

        var request = ClienteBuilder.RegistrarClienteBuild();
        request.Email = "felip";

        var result = await validator.ValidateAsync(request);


        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e =>
            e.PropertyName == "Email" && e.ErrorMessage == ResourceMessagesExceptions.EMAIL_INVALIDO);
    }

    [Fact]
    public async Task RegistrarCliente_CPFVazio_DeveRetornarErrorDeValidacao()
    {
        var request = ClienteBuilder.RegistrarClienteBuild();
        var validator = new RegistrarClienteValidator();
        request.CPF = string.Empty;

        var result = await validator.ValidateAsync(request);

        request.CPF.ShouldBeEmpty();
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e =>
            e.PropertyName == "CPF.Length" && e.ErrorMessage == ResourceMessagesExceptions.CPF_INVALIDO);
    }

    [Fact]
    public async Task RegistrarCliente_NomeVazio_DeveRetornarErroDeValidacao()
    {
        var validator = new RegistrarClienteValidator();

        var request = ClienteBuilder.RegistrarClienteBuild();

        request.Nome = string.Empty;

        var result = await validator.ValidateAsync(request);


        request.Nome.ShouldBeEmpty();
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e =>
            e.PropertyName == "Nome" &&
            e.ErrorMessage == ResourceMessagesExceptions.NOME_VAZIO
        );
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(0)]
    public async Task RegistrarCliente_SenhaInvalida_DeveRetornarErrorDeValidacao(int tamanhoSenha)
    {
        var validator = new RegistrarClienteValidator();
        var request = ClienteBuilder.RegistrarClienteBuild(tamanhoSenha);

        var result = await validator.ValidateAsync(request);
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(e =>
            e.PropertyName == "Senha.Length" && e.ErrorMessage == ResourceMessagesExceptions.SENHA_VAZIA);
    }
}