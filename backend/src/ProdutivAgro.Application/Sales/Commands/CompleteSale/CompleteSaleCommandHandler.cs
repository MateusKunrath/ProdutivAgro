using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Commands.CompleteSale;

public sealed class CompleteSaleCommandHandler(
    ISalesUpdateOnlyRepository salesUpdateOnlyRepository,
    ISalesWriteOnlyRepository salesWriteOnlyRepository,
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

        if (sale.Items.Count == 0)
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.SALE_ITEMS_EMPTY]);
        }

        if (!sale.TryComplete(currentUser.UserId, out var history))
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.SALE_STATUS_INVALID]);
        }

        await salesWriteOnlyRepository.AddStatusHistoryAsync(history!, cancellationToken);
        await unitOfWork.Commit();
        return Unit.Value;
    }
}