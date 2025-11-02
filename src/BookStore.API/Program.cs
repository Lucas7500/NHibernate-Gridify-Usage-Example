using BookStore.Application.IoC;
using BookStore.Infra.IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddDatabaseMigrations(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationUseCases();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BookStore API",
        Version = "v1",
        Description = "API for BookStore application"
    });
});


var app = builder.Build();

app.RunDatabaseMigrations();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
