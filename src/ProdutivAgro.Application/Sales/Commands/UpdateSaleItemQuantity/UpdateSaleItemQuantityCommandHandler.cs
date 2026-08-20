using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Sales.Enums;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Commands.UpdateSaleItemQuantity;

public sealed class UpdateSaleItemQuantityCommandHandler(
    ISalesUpdateOnlyRepository salesUpdateOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateSaleItemQuantityCommand, Unit>
{
    public async Task<Unit> Handle(UpdateSaleItemQuantityCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var sale = await salesUpdateOnlyRepository.GetByIdAsync(request.Id, currentUser.OrganizationId,
            cancellationToken);
        if (sale is null)
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_NOT_FOUND);
        }

        if (sale.Status != SaleStatus.Draft)
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.SALE_STATUS_INVALID]);
        }

        if (!sale.UpdateItemQuantity(request.SaleItemId, request.Quantity))
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_ITEM_NOT_FOUND);
        }

        await unitOfWork.Commit();
        return Unit.Value;
    }

    private static async Task Validate(UpdateSaleItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var result = await new UpdateSaleItemQuantityCommandValidator().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}