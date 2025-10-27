using EasyNetQ;
using EShop.Catalog.Domain.Interfaces;
using EShop.Catalog.Infrestructure.Context;
using EShop.Catalog.Infrestructure.Repositories;
using EShop.Shared.EventBus;
using EShop.Shared.EventBus.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.Api.Extensions;

public static class ProgramExtension
{
    public static void AddModules(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.AddDbContext(configuration);
        builder.AddDependeciInjection(configuration);
    }

    private static void AddDbContext(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var server = Environment.GetEnvironmentVariable("DbServer");
        var port = Environment.GetEnvironmentVariable("DbPort");
        var password = Environment.GetEnvironmentVariable("Password");

        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        builder.Services.AddDbContext<CatalogContext>(options =>
        {
            options.UseSqlServer(connectionString,
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                    );
            });
        });
    }

    private static void AddDependeciInjection(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var subscriptionClientName = configuration["SubscriptionClientName"]!;

        builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();

        builder.Services.AddSingleton<IMessageBus, EventBusRabbitMQ>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

            return new EventBusRabbitMQ(eventBusSubcriptionsManager, subscriptionClientName);
        });
        builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
    }

    public static void ApplyMigrations(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();

        var pendingMigrations = dbContext.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
            dbContext.Database.Migrate();
        else
            Console.WriteLine("No pending migrations found.");
    }
}