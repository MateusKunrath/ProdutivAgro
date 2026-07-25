using FluentValidation.Results;
using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Identity.Commands.ChangePassword;

public sealed class ChangePasswordCommandHandler(
    IUsersUpdateReadOnlyRepository usersUpdateReadOnlyRepository,
    IRefreshTokensUpdateOnlyRepository refreshTokensUpdateOnlyRepository,
    IUnitOfWork unitOfWork,
    ICurrentUser currentUser,
    IPasswordEncrypter passwordEncrypter) : IRequestHandler<ChangePasswordCommand, Unit>
{
    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await usersUpdateReadOnlyRepository.GetByIdAsync(currentUser.UserId, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
        }

        await Validate(request, user, cancellationToken);

        user.SetPasswordHash(passwordEncrypter.Encrypt(request.NewPassword));

        await unitOfWork.ExecuteInTransactionAsync(async cancelToken =>
        {
            usersUpdateReadOnlyRepository.Update(user);

            await refreshTokensUpdateOnlyRepository.RevokeAllActiveByUserIdAsync(currentUser.UserId, cancelToken);
        }, cancellationToken);

        return Unit.Value;
    }

    private async Task Validate(ChangePasswordCommand request, User user, CancellationToken cancellationToken)
    {
        var result = await new ChangePasswordCommandValidator().ValidateAsync(request, cancellationToken);

        var passwordMatch = passwordEncrypter.Verify(request.CurrentPassword, user.Password);
        if (!passwordMatch)
        {
            result.Errors.Add(new ValidationFailure(string.Empty,
                ResourceErrorMessages.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
        }

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}