using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Shared;
using ProdutivAgro.Domain.Identity.Extensions;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Identity.Queries.GetCurrentUser;

public sealed class GetCurrentUserQueryHandler(
    IUsersReadOnlyRepository usersReadOnlyRepository,
    ICurrentUser currentUser) : IRequestHandler<GetCurrentUserQuery, GetCurrentUserResult>
{
    public async Task<GetCurrentUserResult> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await usersReadOnlyRepository.GetByIdAsync(currentUser.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
        }

        return new GetCurrentUserResult
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Organization = new OrganizationIdResult
            {
                Id = user.OrganizationId,
            },
            Role = user.Role.RoleToString(),
            Status = user.Active.StatusToString(),
        };
    }
}