using AutoMapper;
using ProdutivAgro.Communication.Requests;
using ProdutivAgro.Communication.Requests.Users;
using ProdutivAgro.Communication.Responses;
using ProdutivAgro.Domain.Entities;

namespace ProdutivAgro.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestOrganizationJson, Organization>();
        CreateMap<RequestCreateUserJson, User>();
    }

    private void EntityToResponse()
    {
        CreateMap<Organization, ResponseCreatedOrganizationJson>();
    }
}