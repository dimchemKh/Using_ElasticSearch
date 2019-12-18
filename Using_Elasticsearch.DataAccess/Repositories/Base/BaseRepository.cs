using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Repositories.Base.Interfaces;
using DataAnnotation = System.ComponentModel.DataAnnotations.Schema;

namespace Using_Elasticsearch.DataAccess.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IOptions<ConnectionConfig> _options;
        protected string tableName;
        public BaseRepository(IOptions<ConnectionConfig> options)
        {
            _options = options;
            tableName = typeof(TEntity).GetCustomAttribute<DataAnnotation.TableAttribute>().Name;
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            IEnumerable<string> columnNames = typeof(TEntity).GetProperties().Select(x => x.GetCustomAttribute<ColumnAttribute>().Name);

            var columnNamesStr = string.Join(", ", columnNames.Select(c => c));
            var valuesStr = string.Join(", ", columnNames.Select((elem, index) => "@" + columnNames.ElementAt(index)));

            var sql = $"INSERT INTO {tableName} ({columnNamesStr}) VALUES ({valuesStr})";

            using (IDbConnection connection = GetSqlConnection())
            {
               return await connection.ExecuteAsync(sql, entity);
            }
        }
        public Task<int> DeleteAsync(TEntity entity) => throw new NotImplementedException();
        public Task<int> SaveAsync() => throw new NotImplementedException();
        public Task<int> UpdateAsync(TEntity entity) => throw new NotImplementedException();
        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection(_options.Value.ConnectionDb);
        }
    }
}
