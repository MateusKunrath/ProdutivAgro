using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Identity.Commands.ChangeOrganizationResponsible;

public sealed class ChangeOrganizationResponsibleCommandHandler(
    IOrganizationsUpdateReadOnlyRepository organizationsUpdateReadOnlyRepository,
    IUsersReadOnlyRepository usersReadOnlyRepository,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<ChangeOrganizationResponsibleCommand, Unit>
{
    public async Task<Unit> Handle(ChangeOrganizationResponsibleCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var organization = await organizationsUpdateReadOnlyRepository.GetByIdAsync(
            currentUser.OrganizationId, cancellationToken);
        if (organization is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ORGANIZATION_NOT_FOUND);
        }

        if (organization.ResponsibleUserId != currentUser.UserId)
        {
            throw new ForbiddenException(ResourceErrorMessages.ONLY_RESPONSIBLE_CAN_TRANSFER_RESPONSABILITY);
        }

        var newResponsible =
            await usersReadOnlyRepository.GetByIdAsync(request.NewResponsibleUserId, cancellationToken);
        if (newResponsible is null || newResponsible.OrganizationId != organization.Id)
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.RESPONSIBLE_MUST_BE_PART_OF_THE_ORGANIZATION]);
        }

        organization.SetResponsibleUser(newResponsible.Id);
        await unitOfWork.Commit();

        return Unit.Value;
    }

    private static async Task Validate(ChangeOrganizationResponsibleCommand request,
        CancellationToken cancellationToken)
    {
        var result =
            await new ChangeOrganizationResponsibleCommandValidator().ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}