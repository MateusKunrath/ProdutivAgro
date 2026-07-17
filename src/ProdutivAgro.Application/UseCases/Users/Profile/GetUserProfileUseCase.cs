using AutoMapper;
using ProdutivAgro.Communication.Responses.Users;
using ProdutivAgro.Domain.Services.AuthenticatedUser;

namespace ProdutivAgro.Application.UseCases.Users.Profile;

public class GetUserProfileUseCase(
    IAuthenticatedUser authenticatedUser,
    IMapper mapper) : IGetUserProfileUseCase
{
    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await authenticatedUser.Get();
        return mapper.Map<ResponseUserProfileJson>(user);
    }
}