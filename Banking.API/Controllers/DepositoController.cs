using System.Data;
using Banking.API.Attributes;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.Depositar;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.GetAllDepositos;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByData;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByNumero;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByPeriodo;
using Banking.Communication.Requests.Conta.Deposito;
using Banking.Communication.Requests.Conta.Transferencia;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DepositoController : ControllerBase
{
    [HttpPost]
    [Route("Depositar")]
    [ProducesResponseType(typeof(ResponseDepositarJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Depositar([FromBody] RequestExecutarTransferenciaJson request,
        [FromServices] IDepositarUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }

    [HttpGet]
    [Route("GetAllDepositos")]
    [AuthenticatedUser]
    [ProducesResponseType(typeof(List<ResponseDepositarJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDepositos([FromServices] IGetAllDepositosUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpGet]
    [Route("GetDepositoByPeriodo")]
    [AuthenticatedUser]
    [ProducesResponseType(typeof(List<ResponseDepositarJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepositoByPeriodo([FromServices] IGetDepositoByPeriodoUseCase useCase,
        [FromQuery] RequestGetDepositoByPeriodoJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }

    [HttpGet]
    [Route("GetDepositoByData")]
    [AuthenticatedUser]
    [ProducesResponseType(typeof(List<ResponseDepositarJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepositoByData([FromQuery] RequestGetDepositoByDataJson request,
        [FromServices] IGetDepositoByDataUseCase useCase)
    {
        try
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }
        catch (FormatException)
        {
            throw new DataException(ResourceMessagesExceptions.DATA_FORMATO_INVALIDO);
        }
    }

    [HttpGet]
    [Route("GetDepositoByNumero")]
    [AuthenticatedUser]
    [ProducesResponseType(typeof(ResponseDepositarJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepositoByNumero([FromQuery] RequestGetDepositoByNumeroJson request,
        [FromServices] IGetDepositoByNumeroUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}