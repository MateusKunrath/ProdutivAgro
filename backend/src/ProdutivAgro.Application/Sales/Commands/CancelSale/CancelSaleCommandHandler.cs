using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Application.Sales.Shared.Validators;
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
        var sale = await salesUpdateOnlyRepository.GetByIdWithStatusHistoryAsync(request.Id, currentUser.OrganizationId,
            cancellationToken);

        if (sale is null)
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_NOT_FOUND);
        }

        if (!sale.TryCancel(currentUser.UserId, request.Reason, out var history))
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.SALE_STATUS_INVALID]);
        }

        await salesWriteOnlyRepository.AddStatusHistoryAsync(history!, cancellationToken);
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
}