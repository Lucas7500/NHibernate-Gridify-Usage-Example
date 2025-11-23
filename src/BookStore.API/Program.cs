using BookStore.API.Extensions;
using BookStore.Application.IoC;
using BookStore.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddDatabaseMigrations(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationUseCases();

builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.RunDatabaseMigrations();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
