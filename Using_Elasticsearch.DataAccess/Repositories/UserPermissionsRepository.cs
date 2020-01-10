using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Base;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;

namespace Using_Elasticsearch.DataAccess.Repositories
{
    public class UserPermissionsRepository : BaseRepository<UserPermission>, IUserPermissionsRepository
    {
        public UserPermissionsRepository(IOptions<ConnectionConfig> options) : base(options)
        {
        }

        public async Task AddInGroup()
        {

        } 
    }
}
