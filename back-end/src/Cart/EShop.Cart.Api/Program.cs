using EShop.Shared.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModules(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddSwaggerShared();

var app = builder.Build();

app.AddEventBus();
app.SwaggerShared();
app.UseHttpsRedirection();

app.CorsShared();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
