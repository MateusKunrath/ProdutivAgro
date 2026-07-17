using ProdutivAgro.Communication.Requests;
using ProdutivAgro.Communication.Responses;

namespace ProdutivAgro.Application.UseCases.Organizations.Create;

public interface ICreateOrganizationUseCase
{
    Task<ResponseCreatedOrganizationJson> Execute(RequestOrganizationJson request);
}