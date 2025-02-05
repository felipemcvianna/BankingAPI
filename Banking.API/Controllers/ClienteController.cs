using Banking.API.Attributes;
using Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaClienteAutenticado;
using Banking.Application.UseCases.Cliente.AtualizarSenha.AtualizarSenhaEMail;
using Banking.Application.UseCases.Cliente.Deletar;
using Banking.Application.UseCases.Cliente.Ler.ByEmail;
using Banking.Application.UseCases.Cliente.Ler.ByToken;
using Banking.Application.UseCases.Cliente.Registrar;
using Banking.Application.UseCases.Transacao.ExecutarTransacao;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Requests.Transacao;
using Banking.Communication.Response.Cliente;
using Banking.Communication.Response.Transacao;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    [HttpPost]
    [Route("RegistrarCliente")]
    [ProducesResponseType(typeof(ResponseRegistrarClienteJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registrar([FromBody] RequestRegistrarClienteJson request,
        [FromServices] IRegistrarClienteUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Created(String.Empty, result);
    }

    [HttpGet]
    [Route("LerClientePorEmail")]
    [ProducesResponseType(typeof(ResponseRegistrarClienteJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClientByEmail([FromQuery] RequestGetCliente response,
        [FromServices] IGetClienteUseCase useCase)
    {
        var result = await useCase.Execute(response);

        return Ok(result);
    }
    [HttpGet]
    [Route("LerCLientePeloToken")]
    [ProducesResponseType(typeof(ResponseGetClienteByTokenJson), StatusCodes.Status200OK)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetClienteByToken([FromServices] IGetClienteByTokenUseCase _useCase)
    {
        var result = await _useCase.Execute();

        return Ok(result);
    }

    [HttpDelete]
    [Route("DeletarCliente")]
    [ProducesResponseType(typeof(ResponseDeletarClienteJson), StatusCodes.Status200OK)]
    [AuthenticatedUser]
    public async Task<IActionResult> DeletarCliente([FromBody] RequestDeletarClienteJson request,
        [FromServices] IDeletarClienteUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }

    [HttpPatch]
    [Route("AtualizarSenhaCliente")]
    [ProducesResponseType(typeof(ResponseAtualizarClienteJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> AtualizarSenhaCliente([FromBody] RequestAtualizarSenhaClienteJson request,
        [FromServices] IAtualizarSenhaClienteUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
    [HttpPatch]
    [Route("AtualizarSenhaClienteAutenticado")]
    [AuthenticatedUser]
    [ProducesResponseType(typeof(ResponseAtualizarClienteJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> AtualizarSenhaClienteAutenticado([FromBody] RequestAtualizarSenhaClienteAutenticadoJson request,
        [FromServices] IAtualizarSenhaClienteAutenticadoUseCase _useCase)
    {
        var result = await _useCase.Execute(request);

        return Ok(result);
    }
    [HttpPost]
    [Route("ExecutarTransferencia")]
    [ProducesResponseType(typeof(ResponseExecutarTransacaoJson), StatusCodes.Status200OK)]
    [AuthenticatedUser]
    public async Task<IActionResult> ExecutarTransferencia([FromBody] RequestExecutarTransacaoJson request, [FromServices] IExecutarTransacaoUseCase _useCase)
    {
        var result = await _useCase.Execute(request);

        return Ok(result);
    }
}