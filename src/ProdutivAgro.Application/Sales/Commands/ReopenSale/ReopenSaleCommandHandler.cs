using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Application.Sales.Shared.Validators;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Commands.ReopenSale;

public sealed class ReopenSaleCommandHandler(
    ISalesUpdateOnlyRepository salesUpdateOnlyRepository,
    ISalesWriteOnlyRepository salesWriteOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<ReopenSaleCommand, Unit>
{
    public async Task<Unit> Handle(ReopenSaleCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var sale = await salesUpdateOnlyRepository.GetByIdWithStatusHistoryAsync(request.Id, currentUser.OrganizationId,
            cancellationToken);

        if (sale is null)
        {
            throw new NotFoundException(ResourceErrorMessages.SALE_NOT_FOUND);
        }

        if (!sale.TryReopen(currentUser.UserId, request.Reason, out var history))
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.SALE_STATUS_INVALID]);
        }

        await salesWriteOnlyRepository.AddStatusHistoryAsync(history!, cancellationToken);
        await unitOfWork.Commit();
        return Unit.Value;
    }

    private static async Task Validate(ReopenSaleCommand request, CancellationToken cancellationToken)
    {
        var result = await new UpdateSaleStatusValidator<Unit>().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}