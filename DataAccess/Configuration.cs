using Dapper;
using Microsoft.Extensions.DependencyInjection;
using SimonKucherRM.DataAccess.Entities;
using SimonKucherRM.DataAccess.Repositories;
using SimonKucherRM.DataAccess.Repositories.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SimonKucherRM.DataAccess
{
    public class Configuration
    {
        public static void Add(IServiceCollection services, string connectionString)
        {
            AddDependecies(services, connectionString);
            SQlMapper();
        }

        public static void AddDependecies(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnection, SqlConnection>(c => new SqlConnection(connectionString));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ILogExceptionRepository, LogExceptionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserPermissionRepository, UserPermissionRepository>();
            services.AddTransient<IWebAppDataRepository, WebAppDataRepository>();


            services.AddTransient(typeof(IParametrSettingBaseRepository<>), typeof(ParametrSettingBaseRepository<>));
            services.AddTransient<IParkDeanPriceAcceptRepository, ParkDeanPriceAcceptRepository>();
            services.AddTransient<IParkDeanPriceAdjustmentsRepository, ParkDeanPriceAdjustmentsRepository>();
            services.AddTransient<IParkDeanPriceLevelsRepository, ParkDeanPriceLevelsRepository>();
            services.AddTransient<IParkDeanPriceLinkProfilesRepository, ParkDeanPriceLinkProfilesRepository>();
            services.AddTransient<IParkDeanPriceNonstdRatiosRepository, ParkDeanPriceNonstdRatiosRepository>();
            services.AddTransient<IParkDeanTargetLeadTimeRepository, ParkDeanTargetLeadTimeRepository>();
            services.AddTransient<ISKPPropertyMasterRepository, SKPPropertyMasterRepository>();
        }

        private static void SQlMapper()
        {
            SqlMapper.SetTypeMap(typeof(WebAppData), new TitleCaseMap<WebAppData>());
            SqlMapper.SetTypeMap(typeof(ParkDeanPriceAccept), new TitleCaseMap<ParkDeanPriceAccept>());
            SqlMapper.SetTypeMap(typeof(ParkDeanPriceAdjustments), new TitleCaseMap<ParkDeanPriceAdjustments>());
            SqlMapper.SetTypeMap(typeof(ParkDeanPriceLevels), new TitleCaseMap<ParkDeanPriceLevels>());
            SqlMapper.SetTypeMap(typeof(ParkDeanPriceLinkProfiles), new TitleCaseMap<ParkDeanPriceLinkProfiles>());
            SqlMapper.SetTypeMap(typeof(ParkDeanPriceNonstdRatios), new TitleCaseMap<ParkDeanPriceNonstdRatios>());
            SqlMapper.SetTypeMap(typeof(ParkDeanTargetLeadTime), new TitleCaseMap<ParkDeanTargetLeadTime>());
            SqlMapper.SetTypeMap(typeof(SKPPropertyMaster), new TitleCaseMap<SKPPropertyMaster>());
        }
    }

    internal class TitleCaseMap<T> : SqlMapper.ITypeMap where T : new()
    {
        public ConstructorInfo FindExplicitConstructor()
        {
            return null;
        }

        ConstructorInfo SqlMapper.ITypeMap.FindConstructor(string[] names, Type[] types)
        {
            return typeof(T).GetConstructor(Type.EmptyTypes);
        }

        SqlMapper.IMemberMap SqlMapper.ITypeMap.GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            return null;
        }

        SqlMapper.IMemberMap SqlMapper.ITypeMap.GetMember(string columnName)
        {
            string reformattedColumnName = string.Empty;

            foreach (string word in columnName.Replace("_", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                reformattedColumnName += word.First().ToString().ToUpper() + word.Substring(1);
            }

            var prop = typeof(T).GetProperty(reformattedColumnName);

            return prop == null ? null : new PropertyMemberMap(prop);
        }

        class PropertyMemberMap : SqlMapper.IMemberMap
        {
            private readonly PropertyInfo _property;

            public PropertyMemberMap(PropertyInfo property)
            {
                _property = property;
            }
            string SqlMapper.IMemberMap.ColumnName
            {
                get { throw new NotImplementedException(); }
            }

            FieldInfo SqlMapper.IMemberMap.Field
            {
                get { return null; }
            }

            Type SqlMapper.IMemberMap.MemberType
            {
                get { return _property.PropertyType; }
            }

            ParameterInfo SqlMapper.IMemberMap.Parameter
            {
                get { return null; }
            }

            PropertyInfo SqlMapper.IMemberMap.Property
            {
                get { return _property; }
            }
        }
    }
}

