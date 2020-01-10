using System.Collections.Generic;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.DataAccess.Repositories.Base.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> CreateAsync(TEntity entity);
        Task CreateGroup(List<GroupPermission> entities);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> SaveAsync();
    }
}
