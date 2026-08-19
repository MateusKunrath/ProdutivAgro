using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Sales.Entities;
using ProdutivAgro.Domain.Sales.Repositories;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Sales.Commands.CreateSale;

public class CreateSaleCommandHandler(
    ISalesWriteOnlyRepository salesWriteOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var sale = new Sale(currentUser.OrganizationId, currentUser.UserId, request.SoldAt);

        await salesWriteOnlyRepository.AddAsync(sale, cancellationToken);
        await unitOfWork.Commit();

        return new CreateSaleResult
        {
            Id = sale.Id,
        };
    }

    private async Task Validate(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var result = await new CreateSaleCommandValidator().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}