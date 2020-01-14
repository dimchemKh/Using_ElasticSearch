using System.Collections.Generic;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Base.Interfaces;

namespace Using_Elasticsearch.DataAccess.Repositories.Interfaces
{
    public interface IUserPermissionsRepository : IBaseRepository<UserPermission>
    {
        Task<int> AddInGroupAsync(UserPermission permissionModel);
        Task<int> UpdatePermissionAsync(UserPermission permissionModel);
        Task<IList<UserPermission>> GetPermissionsAsync(string userId);
    }
}
