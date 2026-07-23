using AutoMapper;
using FluentValidation.Results;
using MediatR;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Application.Abstractions.Persistence;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Repositories;
using ProdutivAgro.Domain.Identity.Services;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.Identity.Commands.Register;

public class RegisterCommandHandler(
    IUsersReadOnlyRepository usersReadOnlyRepository,
    IUsersWriteOnlyRepository usersWriteOnlyRepository,
    IMapper mapper,
    IPasswordEncrypter passwordEncrypter,
    IJwtTokenGenerator jwtTokenGenerator,
    IUnitOfWork unitOfWork) : IRequestHandler<RegisterCommand, RegisterResult>
{
    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var user = mapper.Map<User>(request);
        user.SetPasswordHash(passwordEncrypter.Encrypt(request.Password));

        await usersWriteOnlyRepository.AddAsync(user);
        await unitOfWork.Commit();

        return new RegisterResult
        {
            Name = user.Name,
            Token = jwtTokenGenerator.Generate(user),
        };
    }

    private static async Task Validate(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await new RegisterCommandValidator().ValidateAsync(request, cancellationToken);

        var emailExists = false;
        // await usersReadOnlyRepository.ExistsUserWithSameEmailAsync(request.Email, cancellationToken);
        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}