using EShop.Shared.Extensions;

namespace EShop.Identity.Extensions;

public static class ProgramExtension
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationShared(configuration["Authentication:Key"]!);
        
        return services;
    }    
}