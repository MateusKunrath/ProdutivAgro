using AutoMapper;
using FluentValidation.Results;
using ProdutivAgro.Communication.Requests.Users;
using ProdutivAgro.Communication.Responses.Users;
using ProdutivAgro.Domain.Entities;
using ProdutivAgro.Domain.Repositories;
using ProdutivAgro.Domain.Repositories.Users;
using ProdutivAgro.Domain.Security.Cryptography;
using ProdutivAgro.Domain.Security.Tokens;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.UseCases.Users.Create;

public class CreateUserUseCase(
    IUsersReadOnlyRepository usersReadOnlyRepository,
    IUsersWriteOnlyRepository usersWriteOnlyRepository,
    IUnitOfWork unitOfWork,
    IAccessTokenGenerator accessTokenGenerator,
    IPasswordEncrypter passwordEncrypter,
    IMapper mapper) : ICreateUserUseCase
{
    public async Task<ResponseCreateUserJson> Execute(RequestCreateUserJson request)
    {
        await Validate(request);

        var user = mapper.Map<User>(request);
        user.Password = passwordEncrypter.Encrypt(request.Password);

        await usersWriteOnlyRepository.Add(user);
        await unitOfWork.Commit();

        return new ResponseCreateUserJson
        {
            Name = user.Name,
            Token = accessTokenGenerator.Generate(user),
        };
    }

    private async Task Validate(RequestCreateUserJson request)
    {
        var result = await new CreateUserValidator().ValidateAsync(request);

        var emailExists = await usersReadOnlyRepository.ExistsActiveUserWithEmail(request.Email);
        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        var phoneNumberExists = await usersReadOnlyRepository.ExistsActiveUserWithPhoneNumber(request.PhoneNumber);
        if (phoneNumberExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.PHONE_NUMBER_ALREADY_EXISTS));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}