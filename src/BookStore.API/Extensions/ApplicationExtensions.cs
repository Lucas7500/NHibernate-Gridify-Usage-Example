using Asp.Versioning;
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
    }
}
