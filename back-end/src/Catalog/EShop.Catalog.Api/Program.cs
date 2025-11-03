using EShop.Catalog.Api.Extensions;
using EShop.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.AddModules(builder.Configuration);
builder.Services.AddSwaggerShared();

var app = builder.Build();

ProgramExtension.ApplyMigrations(app);

app.SwaggerShared();
app.CorsShared();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
