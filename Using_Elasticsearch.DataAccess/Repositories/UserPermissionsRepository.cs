using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Entities.Enums;
using Using_Elasticsearch.DataAccess.Repositories.Base;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;
using UserPermission = Using_Elasticsearch.DataAccess.Entities.UserPermission;

namespace Using_Elasticsearch.DataAccess.Repositories
{
    public class UserPermissionsRepository : BaseRepository<UserPermission>, IUserPermissionsRepository
    {
        private IEnumerable<string> _columnNames;
        public UserPermissionsRepository(IOptions<ConnectionConfig> options) : base(options)
        {
            _columnNames = typeof(UserPermission).GetProperties().Select(x => x.GetCustomAttribute<ColumnAttribute>().Name);
        }

        public async Task<int> AddInGroupAsync(UserPermission permissionModel)
        {
            var columnNamesStr = string.Join(", ", _columnNames.Select(c => c));

            var valuesStr = string.Join(", ", _columnNames.Select((elem, index) => "@" + _columnNames.ElementAt(index)));

            string sql = $@"INSERT INTO {tableName} ({columnNamesStr}) VALUES ({valuesStr}) ";
            
            using (IDbConnection connection = GetSqlConnection())
            {
                return await connection.ExecuteAsync(sql, permissionModel);
            }
        }

        public async Task<int> UpdatePermissionAsync(UserPermission permission)
        {
            var sql = $@"UPDATE {tableName} SET {nameof(permission.CreationDate)} = '{permission.CreationDate}', 
                                                {nameof(permission.CanView)} = '{permission.CanView}', 
                                                {nameof(permission.CanEdit)} = '{permission.CanEdit}', 
                                                {nameof(permission.CanCreate)} = '{permission.CanCreate}' 
                                                {nameof(permission.CanRemove)} = '{permission.CanRemove}' 
                                                WHERE Page = '{(int)permission.Page}' AND UserId = '{permission.UserId}'";

            using (IDbConnection connection = GetSqlConnection())
            {
                return await connection.ExecuteAsync(sql);
            }
        }
        public async Task<IEnumerable<UserPermission>> GetPermissionsAsync(string userId)
        {
            var sql = $@"SELECT * FROM {tableName} WHERE {nameof(UserPermission.UserId)} = '{userId}'";

            var result = new List<dynamic>();

            using (IDbConnection connection = GetSqlConnection())
            {
                result = (await connection.QueryAsync(sql)).ToList();
            }

            var response = result.Select(x => new UserPermission 
            { 
                UserId = x.UserId,
                CanCreate = x.CanCreate,
                CanEdit = x.CanEdit,
                CanView = x.CanView,
                CanRemove = x.CanRemove,
                Page = (PagePermission)x.Page
            })
            .OrderBy(x => x.Page)
            .ToList();
                
            return response;
        }
    }
}
