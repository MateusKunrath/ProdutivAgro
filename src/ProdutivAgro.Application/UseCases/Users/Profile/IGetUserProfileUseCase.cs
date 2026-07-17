using ProdutivAgro.Communication.Responses.Users;

namespace ProdutivAgro.Application.UseCases.Users.Profile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}