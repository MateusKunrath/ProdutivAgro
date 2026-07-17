using ProdutivAgro.Communication.Requests;

namespace ProdutivAgro.Application.UseCases.Organizations.Update;

public interface IUpdateOrganizationUseCase
{
    Task Execute(Guid id, RequestOrganizationJson request);
}