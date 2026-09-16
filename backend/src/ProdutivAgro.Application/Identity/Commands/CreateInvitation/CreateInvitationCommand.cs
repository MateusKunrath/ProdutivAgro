using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Identity.Commands.CreateInvitation;

public sealed class CreateInvitationCommand : IRequest<CreateInvitationResult>, IRequireActiveOrganization
{
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}