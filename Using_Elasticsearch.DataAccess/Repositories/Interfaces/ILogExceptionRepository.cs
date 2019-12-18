using System.Collections.Generic;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Base.Interfaces;

namespace Using_Elasticsearch.DataAccess.Repositories.Interfaces
{
    public interface ILogExceptionRepository : IBaseRepository<LogException>
    {
        Task<IEnumerable<LogException>> GetAllAsync();
    }
}
