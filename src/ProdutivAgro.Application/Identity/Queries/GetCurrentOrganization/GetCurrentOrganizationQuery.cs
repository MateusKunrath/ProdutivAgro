using MediatR;

using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Application.Identity.Queries.GetCurrentOrganization;

public sealed class GetCurrentOrganizationQuery : IRequest<GetCurrentOrganizationResult>, IRequireActiveOrganization { }
