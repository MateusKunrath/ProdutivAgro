using ProdutivAgro.Communication.Requests.Authentication;
using ProdutivAgro.Communication.Responses.Authentication;

namespace ProdutivAgro.Application.UseCases.Authentication.Authenticate;

public interface IAuthenticateUseCase
{
    Task<ResponseAuthenticatedJson> Execute(RequestAuthenticateJson request);
}