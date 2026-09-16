using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Enums;
using ProdutivAgro.Domain.Identity.Extensions;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Identity.Commands.CreateInvitation;

public sealed class CreateInvitationCommandHandler(
    IOrganizationsUpdateReadOnlyRepository organizationsUpdateReadOnlyRepository,
    IUsersReadOnlyRepository usersReadOnlyRepository,
    IInvitationsUpdateOnlyRepository invitationsUpdateOnlyRepository,
    IInvitationsWriteOnlyRepository invitationsWriteOnlyRepository,
    IInvitationTokenService invitationTokenService,
    IInvitationUrlGenerator invitationUrlGenerator,
    ICurrentUser currentUser,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateInvitationCommand, CreateInvitationResult>
{
    public async Task<CreateInvitationResult> Handle(CreateInvitationCommand request,
        CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var organization = await organizationsUpdateReadOnlyRepository.GetByIdAsync(
            currentUser.OrganizationId,
            cancellationToken);

        if (organization is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ORGANIZATION_NOT_FOUND);
        }

        if (organization.ResponsibleUserId != currentUser.UserId)
        {
            throw new ForbiddenException(ResourceErrorMessages.ONLY_RESPONSIBLE_CAN_CREATE_INVITATIONS);
        }

        var userAlreadyExists =
            await usersReadOnlyRepository.ExistsUserWithSameEmailAsync(request.Email, cancellationToken);

        if (userAlreadyExists)
        {
            throw new ErrorOnValidationException([ResourceErrorMessages.EMAIL_ALREADY_EXISTS]);
        }

        var rawToken = invitationTokenService.Generate();
        var now = DateTimeOffset.UtcNow;

        var invitation = new OrganizationInvitation(
            currentUser.OrganizationId,
            request.Email,
            Enum.Parse<UserRole>(request.Role, true),
            invitationTokenService.Hash(rawToken),
            currentUser.UserId,
            invitationTokenService.GetExpirationDate(now));

        await unitOfWork.ExecuteInTransactionAsync(async cancelToken =>
        {
            var pendingInvitation = await invitationsUpdateOnlyRepository.GetPendingByOrganizationIdAndEmailAsync(
                currentUser.OrganizationId,
                request.Email,
                cancelToken);

            pendingInvitation?.Revoke(now);

            await invitationsWriteOnlyRepository.AddAsync(invitation, cancelToken);
        }, cancellationToken);

        return new CreateInvitationResult
        {
            Id = invitation.Id,
            Email = invitation.Email,
            Role = invitation.Role.RoleToString(),
            ExpiresAt = invitation.ExpiresAt,
            InvitationUrl = invitationUrlGenerator.Generate(rawToken),
        };
    }

    private async Task Validate(CreateInvitationCommand request, CancellationToken cancellationToken)
    {
        var result = await new CreateInvitationCommandValidator().ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}