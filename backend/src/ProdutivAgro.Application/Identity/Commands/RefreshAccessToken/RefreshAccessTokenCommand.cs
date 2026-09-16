using MediatR;

namespace ProdutivAgro.Application.Identity.Commands.RefreshAccessToken;

public sealed class RefreshAccessTokenCommand : IRequest<RefreshAccessTokenResult>
{
    public string RefreshToken { get; init; } = string.Empty;
}
