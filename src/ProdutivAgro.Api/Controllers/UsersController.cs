using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Application.UseCases.Users.Create;
using ProdutivAgro.Communication.Requests.Users;
using ProdutivAgro.Communication.Responses;
using ProdutivAgro.Communication.Responses.Users;

namespace ProdutivAgro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreateUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromServices] ICreateUserUseCase useCase,
        [FromBody] RequestCreateUserJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }
}