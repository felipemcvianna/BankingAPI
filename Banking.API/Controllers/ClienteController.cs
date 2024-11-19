using System.Diagnostics.CodeAnalysis;
using Banking.Application.UseCases.Cliente.Ler;
using Banking.Application.UseCases.Cliente.Registrar;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    [HttpPost]
    [Route("RegistrarCliente")]
    [ProducesResponseType(typeof(ResponseRegistrarClienteJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registrar([FromBody] RequestRegistrarClienteJson request, [FromServices] IRegistrarClienteUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Created(String.Empty, result.Nome);
    }

    [HttpGet]
    [Route("LerClientePorEmail")]
    [ProducesResponseType(typeof(ResponseRegistrarClienteJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClientByEmail([FromQuery] RequestGetCliente response, [FromServices] IGetClienteUseCase useCase)
    {
        var result = await useCase.Execute(response);

        return Ok(result);
    }
}