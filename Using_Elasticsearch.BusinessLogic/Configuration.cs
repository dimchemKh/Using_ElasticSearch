using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;
using System;
using Using_Elasticsearch.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.BusinessLogic.Helpers;
using Using_Elasticsearch.BusinessLogic.Helpers.Interfaces;
using Using_Elasticsearch.BusinessLogic.Services;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_ElasticSearch.BusinessLogic.Services;
using Using_ElasticSearch.BusinessLogic.Services.Interfaces;
using DataAccess = Using_Elasticsearch.DataAccess;
using AutoMapper;
using Using_Elasticsearch.BusinessLogic.Automapper;

namespace Using_ElasticSearch.BusinessLogic
{
    public class Configuration
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PasswordConfig>(configuration.GetSection(nameof(PasswordConfig)));
            services.Configure<ConnectionConfig>(configuration.GetSection(nameof(ConnectionConfig)));

            var passwordOptions = services.BuildServiceProvider().GetService<IOptions<PasswordConfig>>();
            var connectionConfig = services.BuildServiceProvider().GetService<IOptions<ConnectionConfig>>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = passwordOptions.Value.RequiredLength;
                options.Password.RequiredUniqueChars = passwordOptions.Value.RequiredUniqueChars;
                options.Password.RequireDigit = passwordOptions.Value.RequireDigit;
                options.Password.RequireUppercase = passwordOptions.Value.RequireUppercase;
                options.Password.RequireLowercase = passwordOptions.Value.RequireLowercase;
                options.Password.RequireNonAlphanumeric = passwordOptions.Value.RequireNonAlphanumeric;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(passwordOptions.Value.DefaultLockoutTimeSpan);
                options.Lockout.MaxFailedAccessAttempts = passwordOptions.Value.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = passwordOptions.Value.AllowedForNewUsers;

                options.User.RequireUniqueEmail = passwordOptions.Value.RequireUniqueEmail;
            });
                        
            AddElasticsearch(services, connectionConfig);

            AddServices(services);

            ConfigureMapper(services);

            DataAccess.Configuration.Add(services, configuration);
        }

        private static void ConfigureMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(map =>
            {
                map.AddProfile(new AdminScreenMapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IJwtFactoryHelper, JwtFactoryHelper>();
            services.AddScoped<IMapperHelper, MapperHelper>();

            services.AddScoped<IElasticsearchService, ElasticsearchService>();
            services.AddScoped<IMainScreenService, MainScreenService>();
            services.AddScoped<IAuthentificationService, AuthentificationService>();
            services.AddSingleton<ILogExceptionService, LogExceptionService>();
            services.AddScoped<IAdminScreenService, AdminScreenService>();
            services.AddScoped<ILogsScreenService, LogsScreenService>();
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
