using Banking.API.Attributes;
using Banking.Application.UseCases.Conta.Transacoes.Saques.LerSaque.GetAllSaques;
using Banking.Application.UseCases.Conta.Transacoes.Saques.Sacar;
using Banking.Communication.Requests.Conta.Transacao;
using Banking.Communication.Response.Conta.Transacao;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SaqueController : ControllerBase
{
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
}