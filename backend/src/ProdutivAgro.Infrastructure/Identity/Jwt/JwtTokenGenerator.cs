using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Domain.Identity.Entities;
using ProdutivAgro.Domain.Identity.Extensions;

namespace ProdutivAgro.Infrastructure.Identity.Jwt;

public class JwtTokenGenerator(uint expirationTimeInMinutes, string signingKey) : IJwtTokenGenerator
{
    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new(CustomClaims.UserId, user.Id.ToString()),
            new(CustomClaims.OrganizationId, user.OrganizationId.ToString()),
            new(CustomClaims.Role, user.Role.RoleToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(signingKey);
        return new SymmetricSecurityKey(key);
    }
}