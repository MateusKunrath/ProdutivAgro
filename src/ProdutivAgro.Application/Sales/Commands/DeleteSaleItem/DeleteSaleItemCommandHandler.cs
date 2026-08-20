using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Sales.Enums;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Commands.DeleteSaleItem;

public class DeleteSaleItemCommandHandler(
    ISalesUpdateOnlyRepository salesUpdateOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteSaleItemCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
    {
        var sale = await salesUpdateOnlyRepository.GetByIdAsync(request.SaleId, currentUser.OrganizationId,
            cancellationToken);
        if (sale is null)
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_NOT_FOUND);
        }

        if (sale.Status != SaleStatus.Draft)
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.SALE_STATUS_INVALID]);
        }

        if (!sale.RemoveItem(request.SaleItemId))
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_ITEM_NOT_FOUND);
        }

        await unitOfWork.Commit();
        return Unit.Value;
    }
}