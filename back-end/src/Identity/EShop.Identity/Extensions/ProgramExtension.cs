using EShop.Identity.Context;
using EShop.Shared.Extensions;
using EShop.Shared.Interfaces;
using EShop.Shared.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.Identity.Extensions;

public static class ProgramExtension
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationShared(configuration["Authentication:Key"]!);
        services.ResolveDependenciInjection();
        services.ResolveDbContext(configuration.GetConnectionString("DefaultConnection")!);

        return services;
    }

    public static IServiceCollection ResolveDependenciInjection(this IServiceCollection services)
    {
        services.AddScoped<INotifier, Notifier>();

        return services;
    }

    public static IServiceCollection ResolveDbContext(this IServiceCollection services, string connection)
    {
        services.AddDbContext<IdentityContext>(options =>
        options.UseSqlServer(connection));

        services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

        return services;
    }
}