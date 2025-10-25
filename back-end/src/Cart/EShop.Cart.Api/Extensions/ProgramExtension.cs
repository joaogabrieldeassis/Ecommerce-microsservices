using EShop.Cart.Api.Application.Queries;
using EShop.Cart.Api.Application.Queries.Commands;
using EShop.Shared.Extensions;

namespace EShop.Cart.Api.Extensions;

public static class ProgramExtension
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationShared(configuration["Authentication:Key"]!);
        services.AddMediator();
        services.AddDbContext(configuration.GetConnectionString("DefaultConnection")!);
        services.ResolveDepenciInjection();

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CartContext>(opt => opt.UseSqlServer(connectionString));

        return services;
    }

    private static IServiceCollection ResolveDepenciInjection(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<AddProductInCartCommand>, AddProductInCartCommandHandler>();
        services.AddScoped<IRequestHandler<CreateCartCommand>, CreateCartCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveProductCartCommand>, RemoveProductCartCommandHandler>();
        services.AddScoped<IRequestHandler<GetCartUserCommand, Models.Cart?>, GetCartUserQuerie>();
        services.AddScoped<INotifier, Notifier>();
        services.AddHttpContextAccessor();

        return services;
    }

    public static void CreateDataForTest(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CartContext>();

        if (!context.Products.Any())
        {
            context.Products.AddRange(
            [
                new Product("Camisa", 12, 99.9m),
                new Product("Tênis", 3, 229.89m),
            ]);

            context.SaveChanges();
        }
    }
}