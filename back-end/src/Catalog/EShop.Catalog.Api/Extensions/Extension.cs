using EShop.Catalog.Domain.Interfaces;
using EShop.Catalog.Infrestructure.Context;
using EShop.Catalog.Infrestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.Api.Extensions;

public static class Extension
{
    public static void AddModules(this WebApplicationBuilder builder)
    {
        builder.AddDbContext();
        builder.AddDependeciInjection();
    }

    private static void AddDbContext(this WebApplicationBuilder builder)
    {
        var server = Environment.GetEnvironmentVariable("DbServer");
        var port = Environment.GetEnvironmentVariable("DbPort");
        var password = Environment.GetEnvironmentVariable("Password");

        var connectionString = $"Server={server},{port};Database=Catalog;User Id=sa;Password={password};TrustServerCertificate=True;";
        builder.Services.AddDbContext<CatalogContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    private static void AddDependeciInjection(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
    }

    public static void ApplyMigrations(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();

            if (pendingMigrations.Any())
                dbContext.Database.Migrate();
            else
                Console.WriteLine("No pending migrations found.");
        }
    }
}