using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using ProdutivAgro.Domain.Entities;
using ProdutivAgro.Domain.Security.Tokens;
using ProdutivAgro.Domain.Services.AuthenticatedUser;
using ProdutivAgro.Infrastructure.DataAccess;
using ProdutivAgro.Infrastructure.Security.Tokens;

namespace ProdutivAgro.Infrastructure.Services.AuthenticatedUser;

public class AuthenticatedUser(ProdutivAgroDbContext dbContext, ITokenProvider tokenProvider) : IAuthenticatedUser
{
    public async Task<User> Get()
    {
        var token = tokenProvider.TokenOnRequest();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type.Equals(CustomClaimTypes.UserId)).Value;
        return await dbContext
                     .Users
                     .AsNoTracking()
                     .FirstAsync(user => user.Id.Equals(Guid.Parse(identifier)));
    }
}