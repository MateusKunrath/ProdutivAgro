using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Application.Sales.Shared.Validators;
using ProdutivAgro.Domain.Sales.Entities;
using ProdutivAgro.Domain.Sales.Enums;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Commands.CancelSale;

public sealed class CancelSaleCommandHandler(
    ISalesUpdateOnlyRepository salesUpdateOnlyRepository,
    ISalesWriteOnlyRepository salesWriteOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<CancelSaleCommand, Unit>
{
    public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);
        var sale = await salesUpdateOnlyRepository.GetByIdAsync(request.Id, currentUser.OrganizationId,
            cancellationToken);

        if (sale is null)
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_NOT_FOUND);
        }

        ValidateSale(sale);

        var saleStatusHistory = sale.Cancel(currentUser.UserId, request.Reason);
        await salesWriteOnlyRepository.AddStatusHistoryAsync(saleStatusHistory, cancellationToken);

        await unitOfWork.Commit();
        return Unit.Value;
    }

    private static async Task Validate(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var result = await new UpdateSaleStatusValidator<Unit>().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }

    private static void ValidateSale(Sale sale)
    {
        if (sale.Status == SaleStatus.Cancelled)
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.SALE_STATUS_INVALID]);
        }
    }
}