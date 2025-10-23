using EShop.Identity.Extensions;
using EShop.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModules(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.CorsShared();
app.SwaggerShared();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
