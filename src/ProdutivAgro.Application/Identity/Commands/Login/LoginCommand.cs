using MediatR;

namespace ProdutivAgro.Application.Identity.Commands.Login;

public sealed class LoginCommand : IRequest<LoginResult>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
