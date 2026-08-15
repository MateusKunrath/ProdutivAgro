using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Sales.Entities;
using ProdutivAgro.Domain.Sales.Enums;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Commands.CompleteSale;

public sealed class CompleteSaleCommandHandler(
    ISalesUpdateOnlyRepository salesUpdateOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<CompleteSaleCommand, Unit>
{
    public async Task<Unit> Handle(CompleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await salesUpdateOnlyRepository.GetByIdAsync(request.Id, currentUser.OrganizationId,
            cancellationToken);

        if (sale is null)
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_NOT_FOUND);
        }

        Validate(sale);

        sale.SetSaleStatus(SaleStatus.Completed);

        await unitOfWork.Commit();
        return Unit.Value;
    }

    private static void Validate(Sale sale)
    {
        if (sale.Status != SaleStatus.Draft)
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.SALE_STATUS_INVALID]);
        }
    }
}