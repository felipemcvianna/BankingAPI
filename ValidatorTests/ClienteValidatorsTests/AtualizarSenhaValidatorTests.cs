using Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaEMail;
using Banking.Exceptions;
using CommomTestsUtilities;
using Shouldly;

namespace ValidatorsTests.ClienteValidatorsTests;

public class AtualizarSenhaValidatorTests
{
    [Fact]
    public async Task AtualizarSenha_ComDadosValidos_DeveSerValido()
    {
        var request = ClienteBuilder.AtualizarSenhaBuild();
        request.ConfirmarNovaSenha = request.NovaSenha;
        var validator = new AtualizarSenhaValidator();

        var result = await validator.ValidateAsync(request);

        request.NovaSenha.ShouldBe(request.ConfirmarNovaSenha);
        request.SenhaAtual.ShouldNotBe(request.NovaSenha);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public async Task AtualizarSenha_NovaSenhaIgualAAtual_DeveRetornarError()
    {
        var request = ClienteBuilder.AtualizarSenhaBuild();
        request.NovaSenha = request.SenhaAtual;
        request.ConfirmarNovaSenha = request.NovaSenha;
        var validator = new AtualizarSenhaValidator();

        var result = await validator.ValidateAsync(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(x =>
            x.PropertyName == "NovaSenha" && x.ErrorMessage == ResourceMessagesExceptions.SENHA_IGUAL);
    }

    [Fact]
    public async Task AtualizarSenha_NovaSenhaDiferenteConfirmarSenha_DeveRetornarError()
    {
        var request = ClienteBuilder.AtualizarSenhaBuild();
        request.ConfirmarNovaSenha = "senhaDiferente";
        var validator = new AtualizarSenhaValidator();

        var result = await validator.ValidateAsync(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(x =>
            x.PropertyName == "ConfirmarNovaSenha" &&
            x.ErrorMessage == ResourceMessagesExceptions.SENHAS_DEVEM_COINCIDIR);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task AtualizarSenha_SenhasVazias_DeveRetornarErroDeValidacao(int tamanhoSenha)
    {
        var request = ClienteBuilder.AtualizarSenhaBuild(tamanhoSenha);
        request.ConfirmarNovaSenha = request.NovaSenha;
        var validator = new AtualizarSenhaValidator();


        var result = await validator.ValidateAsync(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(3);
        result.Errors.ShouldContain(x =>
            x.PropertyName == "SenhaAtual" && x.ErrorMessage == ResourceMessagesExceptions.SENHA_VAZIA);
        
        result.Errors.ShouldContain(x =>
            x.PropertyName == "NovaSenha" && x.ErrorMessage == ResourceMessagesExceptions.SENHA_VAZIA);
        
        result.Errors.ShouldContain(x =>
            x.PropertyName == "ConfirmarNovaSenha" && x.ErrorMessage == ResourceMessagesExceptions.SENHA_VAZIA);
    }

    [Fact]
    public async Task AtualizarSenha_EmailVazio_DeveRetornarErroDeValidacao()
    {
        var request = ClienteBuilder.AtualizarSenhaBuild();
        request.ConfirmarNovaSenha = request.NovaSenha;
        request.Email = string.Empty;
        var validator = new AtualizarSenhaValidator();

        var result = await validator.ValidateAsync(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(2);
        result.Errors.ShouldContain(x =>
            x.PropertyName == "Email" && x.ErrorMessage == ResourceMessagesExceptions.EMAIL_VAZIO);
        
        result.Errors.ShouldContain(x =>
            x.PropertyName == "Email" && x.ErrorMessage == ResourceMessagesExceptions.EMAIL_INVALIDO);
    }
    [Fact]
    public async Task AtualizarSenha_EmailInvalido_DeveRetornarErroDeValidacao()
    {
        var request = ClienteBuilder.AtualizarSenhaBuild();
        request.ConfirmarNovaSenha = request.NovaSenha;
        request.Email = "teste";
        var validator = new AtualizarSenhaValidator();

        var result = await validator.ValidateAsync(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors.ShouldContain(x =>
            x.PropertyName == "Email" && x.ErrorMessage == ResourceMessagesExceptions.EMAIL_INVALIDO);
    }
}