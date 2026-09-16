using MediatR;

namespace ProdutivAgro.Application.Identity.Queries.GetCurrentUser;

public sealed class GetCurrentUserQuery : IRequest<GetCurrentUserResult>;
