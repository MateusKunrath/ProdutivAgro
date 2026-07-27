using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Behaviors;

public sealed class ActiveOrganizationBehavior<TRequest, TResponse>(
    ICurrentUser currentUser,
    IOrganizationsReadOnlyRepository organizationsReadOnlyRepository)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is IRequireActiveOrganization)
        {
            var organizationIsActive = await organizationsReadOnlyRepository.ExistsAndIsActiveAsync(
                currentUser.OrganizationId,
                cancellationToken);

            if (!organizationIsActive)
            {
                throw new NotFoundException(ResourceErrorMessages.ORGANIZATION_NOT_FOUND);
            }
        }

        return await next(cancellationToken);
    }
}
