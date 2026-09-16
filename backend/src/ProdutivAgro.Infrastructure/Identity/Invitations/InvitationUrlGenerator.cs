using Microsoft.Extensions.Options;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Infrastructure.Identity.Invitations;

public sealed class InvitationUrlGenerator(IOptions<InvitationSettings> settings) : IInvitationUrlGenerator
{
    public string Generate(string token)
    {
        return $"{settings.Value.AcceptanceUrl}?token={Uri.EscapeDataString(token)}";
    }
}