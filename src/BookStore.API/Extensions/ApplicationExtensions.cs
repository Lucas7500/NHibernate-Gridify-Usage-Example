using Asp.Versioning;
using BookStore.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

namespace BookStore.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static void AddRoutingAdditionalConfiguration(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BookStore API",
                    Version = "v1"
                });
            });
        }

        public static void UseSwaggerInDevelopment(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API v1");
                    options.RoutePrefix = "swagger";
                });
            }
        }

        public static void AddExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    Exception? exception = context.Features
                        .Get<IExceptionHandlerFeature>()?.Error;

                    if (exception is null) 
                        return;

                    (int status, string title, string type) = exception switch
                    {
                        BusinessRuleValidationException =>
                            (StatusCodes.Status400BadRequest,
                             "Business rule violation",
                             "https://tools.ietf.org/html/rfc7231#section-6.5.1"),

                        _ =>
                            (StatusCodes.Status500InternalServerError,
                             "Internal Server Error",
                             "https://tools.ietf.org/html/rfc7231#section-6.6.1")
                    };

                    context.Response.StatusCode = status;

                    ProblemDetails problemDetails = new()
                    {
                        Status = status,
                        Title = title,
                        Type = type,
                        Instance = context.Request.Path,
                    };

                    problemDetails.Extensions["traceId"] = context.TraceIdentifier;

                    if (app.ApplicationServices
                        .GetRequiredService<IHostEnvironment>()
                        .IsDevelopment())
                    {
                        problemDetails.Detail = exception.Message;
                        problemDetails.Extensions["exceptionType"] = exception.GetType().Name;
                        problemDetails.Extensions["stackTrace"] = exception.StackTrace?.TrimStart();
                    }

                    await context.Response.WriteAsJsonAsync(problemDetails);
                });
            });
        }
    }
}
