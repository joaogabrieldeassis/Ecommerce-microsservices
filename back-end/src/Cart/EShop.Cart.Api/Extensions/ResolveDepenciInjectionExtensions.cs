using EShop.Cart.Api.Application.IntegrationsEvents.Events;
using EShop.Cart.Api.Application.IntegrationsEvents.Handlers;
using EShop.Cart.Api.Application.Queries;
using EShop.Cart.Api.Application.Queries.Commands;
using EShop.Shared.EventBus;
using EShop.Shared.EventBus.Abstraction;
using EShop.Shared.EventBus.Interfaces;

namespace EShop.Cart.Api.Extensions;

public static class ResolveDepenciInjectionExtensions
{

    public static IServiceCollection ResolveDepenciInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotifier, Notifier>();

        services.AddHttpContextAccessor();

        services.ResolveCommands();
        services.ResolveIntegrationsEnvents(configuration["SubscriptionClientName"]!);
        services.AddDbContext(configuration.GetConnectionString("DefaultConnection")!);

        return services;
    }

    private static IServiceCollection ResolveCommands(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<AddProductInCartCommand>, AddProductInCartCommandHandler>();
        services.AddScoped<IRequestHandler<CreateCartCommand>, CreateCartCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveProductCartCommand>, RemoveProductCartCommandHandler>();
        services.AddScoped<IRequestHandler<GetCartUserCommand, Models.Cart?>, GetCartUserQuerie>();
        services.AddScoped<INotifier, Notifier>();

        return services;
    }

    private static IServiceCollection ResolveIntegrationsEnvents(this IServiceCollection services, string subscriptionClientName)
    {
        services.AddSingleton<IMessageBus, EventBusRabbitMQ>(sp =>
        {
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            return new EventBusRabbitMQ(eventBusSubcriptionsManager, subscriptionClientName, sp);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        services.AddTransient<IIntegrationEventHandler<ProductCreatedIntegrationEvent>, ProductCreatedIntegrationEventHandler>();
        services.AddTransient<IIntegrationEventHandler<ProductDeletedIntegrationEvent>, ProductDeletedIntegrationEventHandler>();
        services.AddTransient<IIntegrationEventHandler<ProductUpdatedIntegrationEvent>, ProductUpdatedIntegrationEventHandler>();
        services.AddTransient<ProductCreatedIntegrationEventHandler>();
        services.AddTransient<ProductDeletedIntegrationEventHandler>();
        services.AddTransient<ProductUpdatedIntegrationEventHandler>();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CartContext>(opt => opt.UseSqlServer(connectionString));

        return services;
    }
}