using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Contracts.Errors;
using ProdutivAgro.Api.Contracts.Organizations;
using ProdutivAgro.Application.Identity.Commands.ChangeOrganizationResponsible;
using ProdutivAgro.Application.Identity.Queries.GetCurrentOrganization;

namespace ProdutivAgro.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public sealed class OrganizationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("Current")]
    [ProducesResponseType(typeof(GetCurrentOrganizationResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCurrentOrganizationQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [Route("ChangeResponsible")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ChangeResponsible(
        ChangeOrganizationResponsibleRequest request,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new ChangeOrganizationResponsibleCommand
        {
            NewResponsibleUserId = request.NewResponsibleUserId,
        }, cancellationToken);

        return NoContent();
    }
}
