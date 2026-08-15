using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Contracts.Errors;
using ProdutivAgro.Api.Contracts.Sales;
using ProdutivAgro.Application.Sales.Commands.CompleteSale;
using ProdutivAgro.Application.Sales.Commands.CreateSale;
using ProdutivAgro.Application.Sales.Queries.GetSales;

namespace ProdutivAgro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSale(CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateSaleCommand
        {
            Items = request.Items,
        }, cancellationToken);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetSalesResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetSales(CancellationToken cancellationToken, [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await mediator.Send(
            new GetSalesQuery(pageNumber, pageSize),
            cancellationToken);

        if (result.Items.Count != 0)
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpPost]
    [Route("{id:guid}/Complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompleteSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new CompleteSaleCommand
        {
            Id = id,
        }, cancellationToken);

        return NoContent();
    }
}