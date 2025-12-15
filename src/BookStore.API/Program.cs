using BookStore.API.Extensions;
using BookStore.Application.IoC;
using BookStore.Infra.IoC;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddDatabaseMigrations(builder.Configuration);
builder.Services.AddUnitOfWork();
builder.Services.AddApplicationUseCases();

builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.RunDatabaseMigrations();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.AddExceptionHandler();
app.Run();
