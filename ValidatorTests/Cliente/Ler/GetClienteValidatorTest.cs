using Banking.Application.UseCases.Cliente.Ler;
using CommomTestUtilities.Requests;
using FluentAssertions;
using Xunit;

namespace ValidatorTests.Cliente.Ler;

public class GetClienteValidatorTest
{
    [Fact]
    public void Sucess()
    {
        var request = RequestLerClienteJsonBuilder.Build();

        var validator = new GetClienteValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void LerCliente_FormatoEmailInvalido_DeveRetornarErroDeValidacao()
    {
        var validator = new GetClienteValidator();
        var request = RequestLerClienteJsonBuilder.Build();
        request.Email = "@.com";
      
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals("FORMATO DO EMAIL INVÁLIDO"));
    }
    [Fact]
    public void LerCliente_EmailVazio_DeveRetornarErroDeValidacao()
    {
        var validator = new GetClienteValidator();
        var request = RequestLerClienteJsonBuilder.Build();
        request.Email = string.Empty;
        
        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(@"O CAMPO ""EMAIL"" DEVE SER PREENCHIDO"));
    }
}