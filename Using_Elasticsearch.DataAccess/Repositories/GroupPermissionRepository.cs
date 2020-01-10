using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Base;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;

namespace Using_Elasticsearch.DataAccess.Repositories
{
    public class GroupPermissionRepository : BaseRepository<GroupPermission>, IGroupPermissionRepository
    {
        public GroupPermissionRepository(IOptions<ConnectionConfig> options) : base(options)
        {
        }

        public async Task CreateGroups(List<GroupPermission> groups)
        {
            //IEnumerable<string> columnNames = typeof(GroupPermission).GetProperties().Select(x => x.GetCustomAttribute<ColumnAttribute>().Name);

            //var columnNamesStr = string.Join(", ", columnNames.Select(c => c));
            //var valuesStr = string.Join(", ", columnNames.Select((elem, index) => "@" + columnNames.ElementAt(index)));

            //var sql = $"INSERT INTO {tableName} ({columnNamesStr}) VALUES ({valuesStr})";

            //using (IDbConnection connection = GetSqlConnection())
            //{
            //    return await connection.ExecuteAsync(sql, entity);
            //}
        }
    }
}
