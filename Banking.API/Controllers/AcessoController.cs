using Banking.Application.UseCases.Acesso.Login;
using Banking.Communication.Requests.Login;
using Banking.Communication.Response.Cliente;
using Banking.Communication.Response.Login;
using Microsoft.AspNetCore.Mvc;


namespace Banking.API.Controllers
{
    public class AcessoController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] RequestLoginJson request, [FromServices] ILoginUseCase useCase)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
        //
        // [HttpPost]
        // [Route("Logout")]
        // [ProducesResponseType(typeof(ResponseRegistrarClienteJson), StatusCodes.Status201Created)]
        // public async Task<IActionResult> Logout([FromBody] RequestLoginJson request)
        // {
        //     return Ok();
        // }
    }
}
