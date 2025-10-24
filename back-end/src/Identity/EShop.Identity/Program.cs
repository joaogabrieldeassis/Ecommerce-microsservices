using EShop.Identity.Extensions;
using EShop.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModules(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.SwaggerShared();
app.UseHttpsRedirection();

app.CorsShared();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
