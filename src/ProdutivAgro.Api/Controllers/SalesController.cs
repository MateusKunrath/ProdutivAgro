using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Contracts.Sales;
using ProdutivAgro.Application.Sales.Commands.CreateSale;
using ProdutivAgro.Communication.Responses;

namespace ProdutivAgro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateSale(CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateSaleCommand
        {
            Items = request.Items,
        }, cancellationToken);

        return Created(string.Empty, result);
    }
}