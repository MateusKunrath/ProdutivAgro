using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Contracts.Errors;
using ProdutivAgro.Api.Contracts.Identity;
using ProdutivAgro.Application.Identity.Commands.ChangePassword;
using ProdutivAgro.Application.Identity.Queries.GetCurrentUser;

namespace ProdutivAgro.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public sealed class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("Current")]
    [ProducesResponseType(typeof(GetCurrentUserResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCurrentUserQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Route("ChangePassword")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new ChangePasswordCommand
        {
            CurrentPassword = request.CurrentPassword,
            NewPassword = request.NewPassword,
        }, cancellationToken);

        return NoContent();
    }
}
