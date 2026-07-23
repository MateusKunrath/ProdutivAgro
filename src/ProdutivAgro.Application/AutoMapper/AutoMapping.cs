using AutoMapper;
using ProdutivAgro.Application.Identity.Commands.Register;
using ProdutivAgro.Domain.Identity.Entities;

namespace ProdutivAgro.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
    }

    private void RequestToEntity()
    {
        CreateMap<RegisterCommand, User>().ForMember(dest => dest.Password, config => config.Ignore());
    }
}