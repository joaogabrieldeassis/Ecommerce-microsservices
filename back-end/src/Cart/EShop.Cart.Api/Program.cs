using EShop.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModules(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.SwaggerShared();
app.UseHttpsRedirection();

app.CorsShared();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
