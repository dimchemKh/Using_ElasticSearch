using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Entities.Base;

namespace Using_Elasticsearch.DataAccess.Repositories.Base.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<int> CreateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> SaveAsync();
    }
}
