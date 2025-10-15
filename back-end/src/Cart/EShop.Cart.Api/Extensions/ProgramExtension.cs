namespace EShop.Cart.Api.Extensions;

public static class ProgramExtension
{
    public static IServiceCollection AddModules(this IServiceCollection services)
    {
        services.AddMediator();
        services.AddDbContext();
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

    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") 
                               ?? throw new InvalidOperationException("The connection string was not found in the environment variables");

        services.AddDbContext<CartContext>(opt => opt.UseSqlServer(connectionString));

        return services;
    }

    private static IServiceCollection ResolveDepenciInjection(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<AddProductInCartCommand>, AddProductInCartCommandHandler>();
        services.AddScoped<IRequestHandler<CreateCartCommand>, CreateCartCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveProductCartCommand>, RemoveProductCartCommandHandler>();
        services.AddScoped<INotifier, Notifier>();

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