using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Contracts.Identity;
using ProdutivAgro.Application.Identity.Commands.Register;

namespace ProdutivAgro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterCommand
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role,
            OrganizationId = request.OrganizationId,
        };

        var result = await mediator.Send(command, cancellationToken);

        return Created(string.Empty, result);
    }
}