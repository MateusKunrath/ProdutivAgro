using MediatR;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Products.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IProductsReadOnlyRepository productsReadOnlyRepository,
    IProductsWriteOnlyRepository productsWriteOnlyRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productsReadOnlyRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
        {
            throw new NotFoundException(ResourceErrorMessages.PRODUCT_NOT_FOUND);
        }

        productsWriteOnlyRepository.Remove(product);
        await unitOfWork.Commit();

        return Unit.Value;
    }
}