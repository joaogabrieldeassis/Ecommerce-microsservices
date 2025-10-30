using EShop.Cart.Api.Application.IntegrationsEvents.Events;
using EShop.Cart.Api.Application.IntegrationsEvents.Handlers;
using EShop.Shared.EventBus.Interfaces;
using EShop.Shared.Extensions;

namespace EShop.Cart.Api.Extensions;

public static class ProgramExtension
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationShared(configuration["Authentication:Key"]!);
        services.AddMediator();
        services.ResolveDepenciInjection(configuration);

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

    public static void AddEventBus(this WebApplication app)
    {
        var eventBus = app.Services.GetRequiredService<IMessageBus>();

        eventBus.SubscribeAsync<ProductCreatedIntegrationEvent, ProductCreatedIntegrationEventHandler>()
                .GetAwaiter().GetResult();
    }

    public static void CreateDataForTest(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CartContext>();

        if (!context.Products.Any())
        {
            context.Products.AddRange(
            [
                new Product(Guid.NewGuid(), "Camisa", 12, 99.9m),
                new Product(Guid.NewGuid(),                                
                 "Tênis", 3, 229.89m),
            ]);

            context.SaveChanges();
        }
    }
}