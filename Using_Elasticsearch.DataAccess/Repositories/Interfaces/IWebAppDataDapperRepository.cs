using System.Collections.Generic;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Entities;

namespace Using_Elastic.DataAccess.Repositories.Interfaces
{
    public interface IWebAppDataDapperRepository
    {
        Task<IEnumerable<WebAppData>> GetRangeAsync(int from, int count);
    }
}
