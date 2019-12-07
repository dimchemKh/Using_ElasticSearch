using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Configs;
using Using_Elastic.DataAccess.Entities;
using Using_Elastic.DataAccess.Repositories.Interfaces;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System;

namespace Using_Elastic.DataAccess.Repositories
{
    public class WebAppDataDapperRepository : IWebAppDataDapperRepository
    {
        private readonly IOptions<ConnectionConfig> _options;
        protected string tableName;
        public WebAppDataDapperRepository(IOptions<ConnectionConfig> options)
        {
            _options = options;
            tableName = typeof(WebAppData).GetCustomAttribute<TableAttribute>().Name;
        }

        public async Task<IEnumerable<WebAppData>> GetRangeAsync(int from, int count)
        {
            Stopwatch watch = new Stopwatch();
            //string sql = $@"SKP_Web_App_Data_Select_Specific_Records";
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
