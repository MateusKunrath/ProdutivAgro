using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProdutivAgro.Application.Abstractions.Authentication;

namespace ProdutivAgro.Api.Extensions;

public static class AuthenticationExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            config.MapInboundClaims = false;
            config.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies[
                        ProdutivAgro.Api.Authentication.AuthenticationCookieService.AccessTokenCookieName];

                    return Task.CompletedTask;
                },
            };
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = new TimeSpan(0),
                RoleClaimType = CustomClaims.Role,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!)),
            };
        });
    }
}
