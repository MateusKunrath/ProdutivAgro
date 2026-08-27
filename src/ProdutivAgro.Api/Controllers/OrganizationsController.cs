using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Contracts.Errors;
using ProdutivAgro.Api.Contracts.Organizations;
using ProdutivAgro.Application.Identity.Commands.ChangeOrganizationResponsible;
using ProdutivAgro.Application.Identity.Commands.CreateInvitation;
using ProdutivAgro.Application.Identity.Queries.GetCurrentOrganization;
using ProdutivAgro.Application.Identity.Queries.GetInvitations;
using ProdutivAgro.Domain.Identity.Enums;

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

    [HttpPost]
    [Route("Invitations")]
    [Authorize(Roles = nameof(UserRole.Administrator))]
    [ProducesResponseType(typeof(CreateInvitationResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateInvitation(CreateInvitationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateInvitationCommand
        {
            Email = request.Email,
            Role = request.Role,
        }, cancellationToken);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [Route("Invitations")]
    [Authorize(Roles = nameof(UserRole.Administrator))]
    [ProducesResponseType(typeof(GetInvitationsResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetInvitations(
        CancellationToken cancellationToken,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await mediator.Send(
            new GetInvitationsQuery(pageNumber, pageSize),
            cancellationToken);

        if (result.Items.Count != 0)
        {
            return Ok(result);
        }

        return NoContent();
    }
}