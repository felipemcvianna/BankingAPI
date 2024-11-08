using Banking.Application.UseCases.Cliente.Registrar;
using CommomTestUtilities.Requests;
using FluentAssertions;
using Xunit;

namespace ValidatorTests.Cliente.Registrar;

public class RegistrarClienteValidatorTest
{
    [Fact]
    public void Sucess()
    {
        var validator = new RegistrarClienteValidator();

        var request = RequestClientJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void RegistrarCliente_NomeInvalido_DeveRetornarErroDeValidacao()
    {
        var validator = new RegistrarClienteValidator();
        var request = RequestClientJsonBuilder.Build();
        request.Nome = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void RegistrarCliente_EmailInvalido_DeveRetornarErroDeValidacao()
    {
        var validator = new RegistrarClienteValidator();
        var request = RequestClientJsonBuilder.Build();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void RegistrarCliente_SenhaInvalida_DeveRetornarErroDeValidacao()
    {
        var validator = new RegistrarClienteValidator();
        var request = RequestClientJsonBuilder.Build(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void RegistrarCliente_CPFInvalido_DeveRetornarErroDeValidacao()
    {
        var validator = new RegistrarClienteValidator();
        var request = RequestClientJsonBuilder.Build();
        request.CPF = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
    }
}