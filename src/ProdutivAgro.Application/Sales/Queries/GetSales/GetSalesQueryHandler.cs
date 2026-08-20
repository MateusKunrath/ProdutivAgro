using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Sales.Shared;
using ProdutivAgro.Application.Shared;
using ProdutivAgro.Domain.Sales.Extensions;
using ProdutivAgro.Domain.Sales.Repositories;

namespace ProdutivAgro.Application.Sales.Queries.GetSales;

public class GetSalesQueryHandler(
    ISalesReadOnlyRepository salesReadOnlyRepository,
    ICurrentUser currentUser) : IRequestHandler<GetSalesQuery, GetSalesResult>
{
    public async Task<GetSalesResult> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(request.PageNumber, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, 100);

        var (sales, totalCount) = await salesReadOnlyRepository.GetPagedAsync(
            currentUser.OrganizationId,
            pageNumber,
            pageSize,
            cancellationToken);

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new GetSalesResult
        {
            Items =
            [
                .. sales.Select(sale => new SaleItemResult
                {
                    Id = sale.Id,
                    Status = sale.Status.SaleStatusToString(),
                    TotalAmount = sale.TotalAmount,
                    Organization = new OrganizationIdResult { Id = sale.OrganizationId },
                    SoldAt = sale.SoldAt,
                    CreatedAt = sale.CreatedAt,
                    UpdatedAt = sale.UpdatedAt,
                    SaleUser = new SaleUserResult
                    {
                        Id = sale.CreatedByUser.Id,
                        Name = sale.CreatedByUser.Name,
                        Email = sale.CreatedByUser.Email,
                    },
                }),
            ],
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
        };
    }
}
