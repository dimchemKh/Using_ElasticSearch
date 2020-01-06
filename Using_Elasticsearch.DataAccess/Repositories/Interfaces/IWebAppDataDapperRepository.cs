using System.Collections.Generic;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.DataAccess.Repositories.Interfaces
{
    public interface IWebAppDataDapperRepository
    {
        Task<IEnumerable<WebAppData>> GetRangeAsync(int from, int count);
    }
}
