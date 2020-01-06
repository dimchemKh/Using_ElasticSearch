using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Using_Elasticsearch.Common.Configs;

namespace Using_Elasticsearch.Presentation.Common.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            var swaggerService = services.BuildServiceProvider().GetService<IOptions<SwaggerConfig>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerService.Value.Version, new Info { Title = swaggerService.Value.Title, Version = swaggerService.Value.Version });
            });
        }
        public static void UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerSection = configuration.GetSection(nameof(SwaggerConfig));

            var title = swaggerSection.GetSection(nameof(SwaggerConfig.Title)).Value;
            var version = swaggerSection.GetSection(nameof(SwaggerConfig.Version)).Value;
            var path = swaggerSection.GetSection(nameof(SwaggerConfig.Path)).Value;

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(path, $"{title} {version}");
            });
        }
    }
}
