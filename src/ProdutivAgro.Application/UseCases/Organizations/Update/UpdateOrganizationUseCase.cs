using AutoMapper;
using FluentValidation.Results;
using ProdutivAgro.Communication.Requests;
using ProdutivAgro.Domain.Repositories;
using ProdutivAgro.Domain.Repositories.Organizations;
using ProdutivAgro.Exception;
using ProdutivAgro.Exception.ExceptionsBase;

namespace ProdutivAgro.Application.UseCases.Organizations.Update;

public class UpdateOrganizationUseCase(
    IOrganizationsReadOnlyRepository organizationsReadOnlyRepository,
    IOrganizationsUpdateOnlyRepository organizationsUpdateOnlyRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork
) : IUpdateOrganizationUseCase
{
    public async Task Execute(Guid id, RequestOrganizationJson request)
    {
        await Validate(request);

        var organization = await organizationsUpdateOnlyRepository.GetById(id);
        if (organization is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ORGANIZATION_NOT_FOUND);
        }

        mapper.Map(request, organization);
        organizationsUpdateOnlyRepository.Update(organization);
        await unitOfWork.Commit();
    }

    private async Task Validate(RequestOrganizationJson request)
    {
        var validator = new OrganizationValidator();
        var result = await validator.ValidateAsync(request);

        var organizationNameExists =
            await organizationsReadOnlyRepository.ExistActiveOrganizationWithName(request.Name);
        if (organizationNameExists)
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