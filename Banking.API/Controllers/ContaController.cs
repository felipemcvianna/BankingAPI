using Banking.API.Attributes;
using Banking.Application.UseCases.Cliente.Deletar;
using Banking.Communication.Requests.Cliente;
using Banking.Communication.Response.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
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