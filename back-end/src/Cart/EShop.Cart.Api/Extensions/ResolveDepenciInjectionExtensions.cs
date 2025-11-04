namespace EShop.Cart.Api.Extensions;

public static class ResolveDepenciInjectionExtensions
{

    public static IServiceCollection ResolveDepenciInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.ResolveDependiciInjection();
        services.ResolveCommands();
        services.ResolveIntegrationsEnvents(configuration["SubscriptionClientName"]!);
        services.AddDbContext(configuration.GetConnectionString("DefaultConnection")!);

        return services;
    }

    private static IServiceCollection ResolveDependiciInjection(this IServiceCollection services)
    {
        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<ICartQuerieApplication, CartQuerieApplication>();

        return services;
    }

    private static IServiceCollection ResolveCommands(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<AddProductInCartCommand>, AddProductInCartCommandHandler>();
        services.AddScoped<IRequestHandler<CreateCartCommand>, CreateCartCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveProductCartCommand>, RemoveProductCartCommandHandler>();
        services.AddScoped<IRequestHandler<DecreaseQuantityProductCartCommand, Domain.AggregatesModel.CartAggregate.Cart>, DecreaseQuantityProductCartCommandHandler>();
        services.AddScoped<IRequestHandler<IncreaseQuantityProductCartCommand, Domain.AggregatesModel.CartAggregate.Cart>, IncreaseQuantityProductCartCommandHandler>();


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