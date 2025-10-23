using EShop.Shared.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EShop.Shared.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddAuthenticationShared(this IServiceCollection services, string key)
    {
        var identitySettings = new IdentitySettings(key);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(identitySettings.JwtSecret),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }

    public static void CorsShared(this IApplicationBuilder app)
    {
        app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
    }

    public static void SwaggerShared(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}