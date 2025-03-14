﻿using Banking.API.Attributes;
using Banking.Application.UseCases.Cliente.Deletar;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.Depositar;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.GetAllDepositos;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByData;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByNumero;
using Banking.Application.UseCases.Conta.Transacoes.Deposito.GetDepositoByPeriodo;
using Banking.Application.UseCases.Conta.Transacoes.Sacar.ExecutarSaque;
using Banking.Application.UseCases.Conta.Transacoes.Sacar.LerSaque.GetAllSaques;
using Banking.Application.UseCases.Transacao.ExecutarTranferencia;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Requests.Conta.Deposito;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Cliente;
using Banking.Communication.Response.Conta.Transacao;
using Banking.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        [HttpPost]
        [Route("Transferir")]
        [ProducesResponseType(typeof(ResponseExecutarTransferenciaJson), StatusCodes.Status201Created)]
        [AuthenticatedUser]
        public async Task<IActionResult> Transferencia([FromBody] RequestExecutarTransacaoJson request,
            [FromServices] IExecutarTransferenciaUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("Depositar")]
        [ProducesResponseType(typeof(ResponseDepositarJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Depositar([FromBody] RequestExecutarTransacaoJson request,
            [FromServices] IDepositarUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpDelete]
        [Route("Deletar")]
        [ProducesResponseType(typeof(ResponseDeletarClienteJson), StatusCodes.Status200OK)]
        [AuthenticatedUser]
        public async Task<IActionResult> DeletarConta([FromBody] RequestDeletarClienteJson request,
            [FromServices] IDeletarClienteUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("Sacar")]
        [ProducesResponseType(typeof(ResponseSaqueJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Sacar([FromBody] RequestSaqueJson request,
            [FromServices] ISaqueUseCase useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllSaques")]
        [AuthenticatedUser]
        public async Task<IActionResult> GetAllSaques([FromServices] IGetAllSaquesUseCase useCase)
        {
            var result = await useCase.Execute();

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
            var result = await useCase.Execute(request);

            return Ok(result);
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
}