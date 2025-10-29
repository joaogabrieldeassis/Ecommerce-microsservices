using EShop.Cart.Api.Application.IntegrationsEvents.Events;
using EShop.Cart.Api.Application.IntegrationsEvents.Handlers;
using EShop.Cart.Api.Application.Queries;
using EShop.Cart.Api.Application.Queries.Commands;
using EShop.Shared.EventBus;
using EShop.Shared.EventBus.Abstraction;
using EShop.Shared.EventBus.Interfaces;
using EShop.Shared.Extensions;

namespace EShop.Cart.Api.Extensions;

public static class ProgramExtension
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationShared(configuration["Authentication:Key"]!);
        services.AddMediator();
        services.AddDbContext(configuration.GetConnectionString("DefaultConnection")!);
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

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CartContext>(opt => opt.UseSqlServer(connectionString));

        return services;
    }

    private static IServiceCollection ResolveDepenciInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRequestHandler<AddProductInCartCommand>, AddProductInCartCommandHandler>();
        services.AddScoped<IRequestHandler<CreateCartCommand>, CreateCartCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveProductCartCommand>, RemoveProductCartCommandHandler>();
        services.AddScoped<IRequestHandler<GetCartUserCommand, Models.Cart?>, GetCartUserQuerie>();
        services.AddScoped<INotifier, Notifier>();

        services.AddHttpContextAccessor();

        var subscriptionClientName = configuration["SubscriptionClientName"]!;
        services.AddSingleton<IMessageBus, EventBusRabbitMQ>(sp =>
        {
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            return new EventBusRabbitMQ(eventBusSubcriptionsManager, subscriptionClientName, sp);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        services.AddTransient<EShop.Shared.EventBus.Abstraction.IIntegrationEventHandler<EShop.Cart.Api.Application.IntegrationsEvents.Events.ProductCreatedIntegrationEvent>, ProductCreatedIntegrationEventHandler>();
        services.AddTransient<ProductCreatedIntegrationEventHandler>();

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