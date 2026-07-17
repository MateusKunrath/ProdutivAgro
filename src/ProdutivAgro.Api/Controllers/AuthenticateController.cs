using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Application.UseCases.Authentication.Authenticate;
using ProdutivAgro.Communication.Requests.Authentication;
using ProdutivAgro.Communication.Responses;
using ProdutivAgro.Communication.Responses.Authentication;

namespace ProdutivAgro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseAuthenticatedJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Authenticate(
        [FromServices] IAuthenticateUseCase useCase,
        [FromBody] RequestAuthenticateJson request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }
}