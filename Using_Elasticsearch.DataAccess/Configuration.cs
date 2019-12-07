using System;
using System.Linq;
using System.Reflection;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Using_Elastic.DataAccess.Configs;
using Using_Elastic.DataAccess.Entities;
using Using_Elastic.DataAccess.Repositories;
using Using_Elastic.DataAccess.Repositories.Interfaces;

namespace Using_Elastic.DataAccess
{
    public class Configuration
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionConfig>(configuration.GetSection(nameof(ConnectionConfig)));

            AddRepositories(services);

            SQLMapper();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IWebAppDataDapperRepository, WebAppDataDapperRepository>();
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
