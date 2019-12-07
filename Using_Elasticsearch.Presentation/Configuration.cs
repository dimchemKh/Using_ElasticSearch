using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Using_Elastic.Presentation.Common.Extensions;
using Using_Elastic.Presentation.Common.Configs;
using BusinessLogic = Using_ElasticSearch.BusinessLogic;
using Using_Elasticsearch.Presentation.Common.Configs;
using Using_Elasticsearch.Presentation.Common.Extensions;

namespace Using_Elastic.Presentation
{
    public class Configuration
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<SwaggerConfig>(configuration.GetSection(nameof(SwaggerConfig)));
            services.Configure<CorsConfig>(configuration.GetSection(nameof(CorsConfig)));

            services.AddSwagger();
            services.AddCorsWithOrigin();

            BusinessLogic.Configuration.Add(services, configuration);
        }

        public static void Use(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger(configuration);
            app.UseCors(configuration.GetSection("CorsConfig").GetSection("PolicyName").Value);
        }
    }
}
