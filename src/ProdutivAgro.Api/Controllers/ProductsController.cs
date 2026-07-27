using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProdutivAgro.Api.Contracts.Products;
using ProdutivAgro.Application.Products.Commands.CreateProduct;
using ProdutivAgro.Application.Products.Commands.UpdateProduct;
using ProdutivAgro.Application.Products.Queries.GetProducts;
using ProdutivAgro.Communication.Responses;

namespace ProdutivAgro.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateProductCommand
        {
            Description = request.Description,
            UnitPrice = request.UnitPrice,
            MeasurementUnit = request.MeasurementUnit,
        }, cancellationToken);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetProductsResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetProducts([FromQuery] int pageNumber, [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new GetProductsQuery(pageNumber, pageSize),
            cancellationToken);

        if (result.Items.Count != 0)
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid id,
        UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProductCommand
        {
            Id = id,
            Description = request.Description,
            UnitPrice = request.UnitPrice,
            MeasurementUnit = request.MeasurementUnit,
        }, cancellationToken);

        return NoContent();
    }
}