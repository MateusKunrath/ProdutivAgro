using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Application.UseCases.Organizations.Create;
using ProdutivAgro.Application.UseCases.Organizations.Update;
using ProdutivAgro.Communication.Requests;
using ProdutivAgro.Communication.Responses;

namespace ProdutivAgro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreatedOrganizationJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromServices] ICreateOrganizationUseCase useCase,
        [FromBody] RequestOrganizationJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateOrganizationUseCase useCase, [FromRoute] Guid id,
        [FromBody] RequestOrganizationJson request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }
}