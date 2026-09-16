using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Authentication;
using ProdutivAgro.Api.Contracts.Errors;
using ProdutivAgro.Api.Contracts.Identity;
using ProdutivAgro.Application.Identity.Commands.Login;
using ProdutivAgro.Application.Identity.Commands.Logout;
using ProdutivAgro.Application.Identity.Commands.RefreshAccessToken;
using ProdutivAgro.Application.Identity.Commands.Register;

namespace ProdutivAgro.Api.Controllers;

[Route("api/Auth")]
[ApiController]
public sealed class IdentityController(
    IMediator mediator,
    AuthenticationCookieService authenticationCookieService) : ControllerBase
{
    [HttpPost]
    [Route("Login")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginCommand
        {
            Email = request.Email,
            Password = request.Password,
        }, cancellationToken);

        authenticationCookieService.SetAuthenticationCookies(Response, result.AccessToken, result.RefreshToken);

        return NoContent();
    }

    [HttpPost]
    [Route("Register")]
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RegisterCommand
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            OrganizationName = request.OrganizationName,
        }, cancellationToken);

        authenticationCookieService.SetAuthenticationCookies(Response, result.AccessToken, result.RefreshToken);

        return Created(string.Empty, new RegisterResponse { Name = result.Name });
    }

    [HttpPost]
    [Route("RefreshAccessToken")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshAccessToken(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RefreshAccessTokenCommand
        {
            RefreshToken = authenticationCookieService.GetRefreshToken(Request),
        }, cancellationToken);

        authenticationCookieService.SetAuthenticationCookies(Response, result.AccessToken, result.RefreshToken);

        return NoContent();
    }

    [HttpPost]
    [Route("Logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await mediator.Send(new LogoutCommand
        {
            RefreshToken = authenticationCookieService.GetRefreshToken(Request),
        }, cancellationToken);

        authenticationCookieService.ClearAuthenticationCookies(Response);

        return NoContent();
    }
}
