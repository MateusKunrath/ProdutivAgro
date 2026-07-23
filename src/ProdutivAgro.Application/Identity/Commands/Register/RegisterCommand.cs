using MediatR;

namespace ProdutivAgro.Application.Identity.Commands.Register;

public sealed class RegisterCommand : IRequest<RegisterResult>
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}