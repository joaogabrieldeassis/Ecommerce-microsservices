using EShop.Catalog.Domain.Interfaces;
using EShop.Catalog.Infrestructure.Context;
using EShop.Catalog.Infrestructure.Repositories;
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
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionSql"));
        });
    }

    private static void AddDependeciInjection(this WebApplicationBuilder builder) 
    {
        builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
    }
}