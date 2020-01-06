using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System;
using Using_Elasticsearch.DataAccess.Repositories.Base;
using Using_Elasticsearch.DataAccess.AppContext;

namespace Using_Elasticsearch.DataAccess.Repositories
{
    public class WebAppDataRepository : BaseRepository<WebAppData>, IWebAppDataDapperRepository
    {
        public WebAppDataRepository(IOptions<ConnectionConfig> options) : base(options)
        {
        }

        public async Task<IEnumerable<WebAppData>> GetRangeAsync(int from, int count)
        {
            string sql = $@"GetPartRecords";

            using (IDbConnection db = GetSqlConnection())
            {
                var result = await db.QueryAsync<WebAppData>(sql, new { FromRow = from, CountRows = count }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        protected SqlConnection GetSqlConnection()
        {
            var res = new SqlConnection(_options.Value.ConnectionDb);
            return res;
        }
    }
}
