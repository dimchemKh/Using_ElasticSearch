using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;
using System;
using Using_Elastic.DataAccess.Common.Models;
using Using_Elastic.DataAccess.Entities;
using Using_ElasticSearch.BusinessLogic.Services;
using Using_ElasticSearch.BusinessLogic.Services.Interfaces;
using DataAccess = Using_Elastic.DataAccess;

namespace Using_ElasticSearch.BusinessLogic
{
    public class Configuration
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            DataAccess.Configuration.Add(services, configuration);

            var connectionConfig = services.BuildServiceProvider().GetService<IOptions<ConnectionConfig>>();            

            AddElasticsearch(services, connectionConfig);

            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IElasticsearchService, ElasticsearchService>();
        }

        private static void AddElasticsearch(IServiceCollection services, IOptions<ConnectionConfig> connectionConfig)
        {
            var settings = new ConnectionSettings(new Uri(connectionConfig.Value.ConnectionElastic));

            settings.DefaultMappingFor<WebAppData>(x => x.IndexName(connectionConfig.Value.ElasticIndex));

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
