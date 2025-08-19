using EShop.Catalog.Infrestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Buffers.Text;
using System.ComponentModel;
using System.Reflection;

namespace EShop.Catalog.Api.Extensions;

public static class Extension
{
    public static void AddModules(this WebApplicationBuilder builder)
    {
        builder.AddDbContext();
    }

    private static void AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CatalogContext>(options =>
        {
            options.UseSqlServer(builder.Configuration["ConnectionString"],
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);

                sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });
        });
    }
}