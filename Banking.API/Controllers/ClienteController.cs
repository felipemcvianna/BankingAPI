using Banking.Application.UseCases.Cliente.Registrar;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegistrarClienteJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registrar([FromBody] RequestRegistrarClienteJson request, [FromServices] IRegistrarClienteUseCase useCase)
    {
        var result = await useCase.Execute(request);

        return Created(String.Empty, result.Nome);
    }
}