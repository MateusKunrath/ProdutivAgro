using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Contracts.Identity;
using ProdutivAgro.Application.Identity.Commands.ChangePassword;
using ProdutivAgro.Application.Identity.Commands.Login;
using ProdutivAgro.Application.Identity.Commands.Logout;
using ProdutivAgro.Application.Identity.Commands.RefreshAccessToken;
using ProdutivAgro.Application.Identity.Commands.Register;
using ProdutivAgro.Communication.Responses;

namespace ProdutivAgro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("Login")]
    [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginCommand
        {
            Email = request.Email,
            Password = request.Password,
        }, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("Register")]
    [ProducesResponseType(typeof(RegisterResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RegisterCommand
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            OrganizationName = request.OrganizationName,
        }, cancellationToken);

        return Created(string.Empty, result);
    }

    [HttpPost]
    [Route("RefreshAccessToken")]
    [ProducesResponseType(typeof(RefreshAccessTokenResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshAccessToken(RefreshAccessTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RefreshAccessTokenCommand
        {
            RefreshToken = request.RefreshToken,
        }, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Route("Logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(new LogoutCommand
        {
            RefreshToken = request.RefreshToken,
        }, cancellationToken);

        return NoContent();
    }

    [Authorize]
    [HttpPost]
    [Route("ChangePassword")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
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