﻿using Banking.API.Attributes;
using Banking.Application.UseCases.Cliente.Deletar;
using Banking.Application.UseCases.Transacao.Depositar;
using Banking.Application.UseCases.Transacao.ExecutarTranferencia;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Cliente;
using Banking.Communication.Response.Conta.Transacao;
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
        public async Task<IActionResult> Transferencia([FromBody] RequestExecutarTransacaoJson request, [FromServices] IExecutarTransferenciaUseCase _useCase)
        {
            var result = await _useCase.Execute(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("Depositar")]
        [ProducesResponseType(typeof(ResponseDepositarJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Depositar([FromBody] RequestExecutarTransacaoJson request, [FromServices] IDepositarUseCase _useCase)
        {
            var result = await _useCase.Execute(request);

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

    }
}
