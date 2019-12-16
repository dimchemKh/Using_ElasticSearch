using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Using_Elasticsearch.Common.Configs;

namespace Using_Elasticsearch.Presentation.Common.Extensions
{
    public static class CorsExtension
    {
        public static void AddCorsWithOrigin(this IServiceCollection services)
        {
            var corsService = services.BuildServiceProvider().GetService<IOptions<CorsConfig>>();

            services.AddCors(opt =>
            {
                opt.AddPolicy(corsService.Value.PolicyName, builder =>
                {
                    builder.WithOrigins(corsService.Value.Origins)
                           .AllowCredentials()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
    }
}
