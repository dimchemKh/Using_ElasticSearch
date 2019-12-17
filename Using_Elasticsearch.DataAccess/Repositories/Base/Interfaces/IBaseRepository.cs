using System.Threading.Tasks;

namespace Using_Elasticsearch.DataAccess.Repositories.Base.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> CreateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> SaveAsync();
    }
}
