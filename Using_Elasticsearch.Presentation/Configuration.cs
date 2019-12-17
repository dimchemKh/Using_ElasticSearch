using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Using_Elastic.Presentation.Common.Extensions;
using BusinessLogic = Using_ElasticSearch.BusinessLogic;
using Using_Elasticsearch.Presentation.Common.Extensions;
using Using_Elasticsearch.Common.Configs;
using Using_Elastic.Common.Configs;

namespace Using_Elastic.Presentation
{
    public class Configuration
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CorsConfig>(configuration.GetSection(nameof(CorsConfig)));
            services.Configure<SwaggerConfig>(configuration.GetSection(nameof(SwaggerConfig)));
            services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));
            
            services.AddSwagger();
            services.AddCorsWithOrigin();
            services.AddJwt();

            BusinessLogic.Configuration.Add(services, configuration);
        }

        public static void Use(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger(configuration);
            app.UseCors(configuration.GetSection(nameof(CorsConfig)).GetSection("PolicyName").Value);

            BusinessLogic.Configuration.Use(app);
        }
    }
}
