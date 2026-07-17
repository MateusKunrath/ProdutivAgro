using AutoMapper;
using FluentValidation.Results;
using ProdutivAgro.Communication.Requests;
using ProdutivAgro.Communication.Responses;
using ProdutivAgro.Domain.Entities;
using ProdutivAgro.Domain.Repositories;
using ProdutivAgro.Domain.Repositories.Organizations;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.UseCases.Organizations.Create;

public class CreateOrganizationUseCase(
    IMapper mapper,
    IOrganizationsReadOnlyRepository organizationsReadOnlyRepository,
    IOrganizationsWriteOnlyRepository organizationsWriteOnlyRepository,
    IUnitOfWork unitOfWork) : ICreateOrganizationUseCase
{
    public async Task<ResponseCreatedOrganizationJson> Execute(RequestOrganizationJson request)
    {
        await Validate(request);

        var organization = mapper.Map<Organization>(request);

        await organizationsWriteOnlyRepository.Add(organization);
        await unitOfWork.Commit();

        return new ResponseCreatedOrganizationJson
        {
            Id = organization.Id,
        };
    }

    private async Task Validate(RequestOrganizationJson request)
    {
        var result = await new OrganizationValidator().ValidateAsync(request);

        var nameExists = await organizationsReadOnlyRepository.ExistActiveOrganizationWithName(request.Name);
        if (nameExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.ORGANIZATION_ALREADY_EXISTS));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}