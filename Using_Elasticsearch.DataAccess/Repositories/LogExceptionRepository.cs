using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Base;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;

namespace Using_Elasticsearch.DataAccess.Repositories
{
    public class LogExceptionRepository : BaseRepository<LogException>, ILogExceptionRepository
    {
        public LogExceptionRepository(IOptions<ConnectionConfig> options) : base(options)
        {
        }
        public async Task<IEnumerable<LogException>> GetAllAsync()
        {
            var sql = "SELECT * FROM LogExceptions";
            
            using (IDbConnection connection = GetSqlConnection())
            {
                return await connection.QueryAsync<LogException>(sql);
            }

        }
    }
}
