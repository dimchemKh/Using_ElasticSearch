using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Entities.Base;
using Using_Elasticsearch.DataAccess.Repositories.Base.Interfaces;


namespace Using_Elasticsearch.DataAccess.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly IOptions<ConnectionConfig> _options;
        public BaseRepository(IOptions<ConnectionConfig> options)
        {
            _options = options;
        }
        public async Task<int> CreateAsync(TEntity entity)
        {
            using (var connection = GetSqlConnection())
            {
                var result = await connection.InsertAsync(entity);

                return 1;
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
