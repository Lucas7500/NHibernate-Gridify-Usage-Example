using Asp.Versioning;
using Microsoft.AspNetCore.Diagnostics;
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

        public static void AddExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exception = context.Features
                        .Get<IExceptionHandlerFeature>()?.Error;

                    context.Response.StatusCode = exception switch
                    {
                        _ => StatusCodes.Status500InternalServerError
                    };

                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "An unexpected error occurred",
                        traceId = context.TraceIdentifier
                    });
                });
            });
        }
    }
}
