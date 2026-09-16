using MediatR;

namespace ProdutivAgro.Application.Identity.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<Unit>
{
    public string CurrentPassword { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
}