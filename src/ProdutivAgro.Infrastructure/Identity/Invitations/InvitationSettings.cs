using System.ComponentModel.DataAnnotations;

namespace ProdutivAgro.Infrastructure.Identity.Invitations;

public sealed class InvitationSettings
{
    [Required] [Url] public string AcceptanceUrl { get; init; } = string.Empty;

    [Range(1, 30)] public uint ExpiresDays { get; init; } = 7;
}