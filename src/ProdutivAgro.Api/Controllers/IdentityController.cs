using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Contracts.Identity;
using ProdutivAgro.Application.Identity.Commands.Login;
using ProdutivAgro.Application.Identity.Commands.RefreshAccessToken;
using ProdutivAgro.Application.Identity.Commands.Register;

namespace ProdutivAgro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginCommand
        {
            Email = request.Email,
            Password = request.Password,
        }, cancellationToken);

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request,
        CancellationToken cancellationToken)
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

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAccessToken(
        RefreshAccessTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RefreshAccessTokenCommand
        {
            RefreshToken = request.RefreshToken,
        }, cancellationToken);

        return Ok(result);
    }
}