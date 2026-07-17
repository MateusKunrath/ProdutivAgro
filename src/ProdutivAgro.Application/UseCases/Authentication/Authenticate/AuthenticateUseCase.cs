using ProdutivAgro.Communication.Requests.Authentication;
using ProdutivAgro.Communication.Responses.Authentication;
using ProdutivAgro.Domain.Repositories.Users;
using ProdutivAgro.Domain.Security.Cryptography;
using ProdutivAgro.Domain.Security.Tokens;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.UseCases.Authentication.Authenticate;

public class AuthenticateUseCase(
    IUsersReadOnlyRepository usersReadOnlyRepository,
    IPasswordEncrypter passwordEncrypter,
    IAccessTokenGenerator tokenGenerator) : IAuthenticateUseCase
{
    public async Task<ResponseAuthenticatedJson> Execute(RequestAuthenticateJson request)
    {
        var user = await usersReadOnlyRepository.GetUserByEmailOrPhoneNumber(request.Identifier);
        if (user is null)
        {
            throw new InvalidAuthenticationException();
        }

        var passwordMatch = passwordEncrypter.Verify(request.Password, user.Password);
        if (!passwordMatch)
        {
            throw new InvalidAuthenticationException();
        }

        return new ResponseAuthenticatedJson
        {
            Token = tokenGenerator.Generate(user),
        };
    }
}