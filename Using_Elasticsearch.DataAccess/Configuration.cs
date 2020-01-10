using System;
using System.Linq;
using System.Reflection;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Using_Elasticsearch.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;
using Using_Elasticsearch.DataAccess.AppContext;
using Using_Elasticsearch.DataAccess.DbInitializers;

namespace Using_Elasticsearch.DataAccess
{
    public static class Configuration
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbInitializer>();

            AddContext(services, configuration);

            AddRepositories(services);

            SQLMapper();           
        }

        private static void AddContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetSection(nameof(ConnectionConfig)).GetSection("ConnectionDb").Value));

            services.AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
        }

        public static void EnsureMigrate(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationContext>())
                {
                    if (!context.Database.EnsureCreated())
                    {
                        context.Database.Migrate();
                    }
                }
            }
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IWebAppDataDapperRepository, WebAppDataRepository>();
            services.AddTransient<ILogExceptionRepository, LogExceptionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserPermissionsRepository, UserPermissionsRepository>();
        }

        public static void SQLMapper()
        {
            SqlMapper.SetTypeMap(typeof(WebAppData), new SqlTypeMap<WebAppData>());
        }

        private class SqlTypeMap<T> : SqlMapper.ITypeMap
        {
            public ConstructorInfo FindConstructor(string[] names, Type[] types)
            {
                return typeof(T).GetConstructor(Type.EmptyTypes);
            }

            public ConstructorInfo FindExplicitConstructor()
            {
                return null;
            }

            public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName) => throw new NotImplementedException();

            public SqlMapper.IMemberMap GetMember(string columnName)
            {
                var reformatedColumnName = string.Empty;

                foreach (var word in columnName.Replace("_", " ").Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    reformatedColumnName += word.First().ToString().ToUpper() + word.Substring(1);
                }

                var property = typeof(T).GetProperty(reformatedColumnName);

                return property == null ? null : new PropertyMemberMap(property);
            }

            private class PropertyMemberMap : SqlMapper.IMemberMap
            {
                private readonly PropertyInfo _property;
                public PropertyMemberMap(PropertyInfo property)
                {
                    _property = property;
                }
                public string ColumnName { get; }
                public Type MemberType
                {
                    get { return _property.PropertyType; }
                }
                public PropertyInfo Property
                {
                    get { return _property; }
                }
                public FieldInfo Field { get; }
                public ParameterInfo Parameter { get; }
            }

        }
    }
}
