using MediatR;

namespace ProdutivAgro.Application.Identity.Commands.Logout;

public sealed class LogoutCommand : IRequest<Unit>
{
    public string RefreshToken { get; init; } = string.Empty;
}