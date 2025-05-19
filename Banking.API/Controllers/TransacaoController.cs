using Banking.API.Attributes;
using Banking.Application.UseCases.Conta.Transacoes.Transferencias.ExecutarTranferencia;
using Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetAllTransferencias;
using Banking.Application.UseCases.Conta.Transacoes.Transferencias.GetTransferenciaByData;
using Banking.Communication.Requests.Conta.Transferencia;
using Banking.Communication.Response.Conta.Transacao;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TransacaoController : ControllerBase
{
    [HttpPost]
    [Route("Transferir")]
    [ProducesResponseType(typeof(ResponseExecutarTransferenciaJson), StatusCodes.Status201Created)]
    [AuthenticatedUser]
    public async Task<IActionResult> Transferencia([FromBody] RequestExecutarTransferenciaJson request,
        [FromServices] IExecutarTransferenciaUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }

    [HttpGet]
    [Route("GetallTransferencias")]
    [AuthenticatedUser]
    public async Task<IActionResult> GetAllTransferencias([FromServices] IGetAllTransferenciasUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpGet]
    [Route("GetTransferenciaByData")]
    [AuthenticatedUser]
    public async Task<IActionResult> GetTransferenciaByData([FromQuery] RequestGetTransferenciaByData request,
        IGetTransferenciaByDataUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}