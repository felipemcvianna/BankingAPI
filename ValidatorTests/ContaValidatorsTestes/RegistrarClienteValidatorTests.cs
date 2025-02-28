using Banking.Application.UseCases.Cliente.Registrar;
using Banking.Exceptions;
using CommomTestsUtilities;
using FluentValidation;
using Shouldly;

namespace ValidatorsTests.ContaValidatorsTestes;

public class RegistrarClienteValidatorTests
{
    [Fact]
    public async Task RegistrarCliente_DadosValidos_DeveSerValido()
    {
        var validator = new RegistrarClienteValidator();

        var request = RegistrarClienteBuilder.Build();

        var result = await validator.ValidateAsync(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public async Task RegistrarCliente_EmailVazio_DeveRetornarErrorDeValidacao()
    {
        var validator = new RegistrarClienteValidator();

        var request = RegistrarClienteBuilder.Build();

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

        var request = RegistrarClienteBuilder.Build();
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
        var request = RegistrarClienteBuilder.Build();
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

        var request = RegistrarClienteBuilder.Build();

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
}