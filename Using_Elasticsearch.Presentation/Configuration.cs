using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Using_Elastic.Presentation.Common.Extensions;
using Using_Elastic.Presentation.Common.Models;
using BusinessLogic = Using_ElasticSearch.BusinessLogic;

namespace Using_Elastic.Presentation
{
    public class Configuration
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<SwaggerConfig>(configuration.GetSection(nameof(SwaggerConfig)));

            services.AddSwagger();

            BusinessLogic.Configuration.Add(services, configuration);
        }

        public static void Use(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger(configuration);
        }
    }
}
