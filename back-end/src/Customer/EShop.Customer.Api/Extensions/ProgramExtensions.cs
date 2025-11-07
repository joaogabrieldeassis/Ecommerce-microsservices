using EShop.Customer.Application;
using EShop.Customer.Infra.Data;
using EShop.Shared.EventBus;
using EShop.Shared.EventBus.Interfaces;
using EShop.Shared.Interfaces;
using EShop.Shared.Notifications;
using Microsoft.EntityFrameworkCore;

namespace EShop.Customer.Api.Extensions;

public static class ProgramExtensions
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.ResolveDependiciInjection();
        services.ResolveIntegrationsEnvents(configuration["SubscriptionClientName"]!);
        services.AddDbContext(configuration.GetConnectionString("DefaultConnection")!);

        return services;
    }

    private static IServiceCollection ResolveDependiciInjection(this IServiceCollection services)
    {
        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<ICustomerApplication, CustomerApplication>();

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

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CustomerContext>(opt => opt.UseSqlServer(connectionString));

        return services;
    }
}