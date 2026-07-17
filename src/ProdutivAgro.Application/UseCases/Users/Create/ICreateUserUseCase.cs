using ProdutivAgro.Communication.Requests.Users;
using ProdutivAgro.Communication.Responses.Users;

namespace ProdutivAgro.Application.UseCases.Users.Create;

public interface ICreateUserUseCase
{
    Task<ResponseCreateUserJson> Execute(RequestCreateUserJson request);
}