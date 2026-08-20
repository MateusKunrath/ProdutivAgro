using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Sales.Shared;
using ProdutivAgro.Application.Shared;
using ProdutivAgro.Domain.Products.Extensions;
using ProdutivAgro.Domain.Sales.Extensions;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Queries.GetSaleById;

public class GetSaleByIdQueryHandler(
    ISalesReadOnlyRepository salesReadOnlyRepository,
    ICurrentUser currentUser) : IRequestHandler<GetSaleByIdQuery, GetSaleByIdResult>
{
    public async Task<GetSaleByIdResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await salesReadOnlyRepository.GetByIdAsync(request.Id, currentUser.OrganizationId,
            cancellationToken);
        if (sale is null)
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_NOT_FOUND);
        }

        return new GetSaleByIdResult
        {
            Id = sale.Id,
            Organization = new OrganizationIdResult
            {
                Id = sale.OrganizationId,
            },
            CreatedUser = new SaleUserResult
            {
                Id = sale.CreatedByUser.Id,
                Name = sale.CreatedByUser.Name,
                Email = sale.CreatedByUser.Email,
            },
            UpdatedUser = sale.UpdatedByUser is null
                ? null
                : new SaleUserResult
                {
                    Id = sale.UpdatedByUser.Id,
                    Name = sale.UpdatedByUser.Name,
                    Email = sale.UpdatedByUser.Email,
                },
            Status = sale.Status.SaleStatusToString(),
            Items =
            [
                .. sale.Items.Select(item => new GetSaleItemResult
                {
                    Id = item.Id,
                    ProductId = item.SourceProductId,
                    Description = item.ProductDescription,
                    MeasurementUnit = item.Unit.MeasurementUnitToString(),
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalAmount = item.TotalAmount,
                }),
            ],
            TotalAmount = sale.TotalAmount,
            SoldAt = sale.SoldAt,
            CreatedAt = sale.CreatedAt,
            UpdatedAt = sale.UpdatedAt,
        };
    }
}
