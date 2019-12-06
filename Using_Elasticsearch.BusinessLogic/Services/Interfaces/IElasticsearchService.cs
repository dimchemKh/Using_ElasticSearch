using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Entities;
using Using_Elasticsearch.BusinessLogic.Common.Models;

namespace Using_ElasticSearch.BusinessLogic.Services.Interfaces
{
    public interface IElasticsearchService
    {
        Task IndexDataAsync();
        Task<IEnumerable<WebAppData>> GetRangeAsync(FilterModel filter);
        Task<IEnumerable<WebAppData>> GetSearchTermAsync(FIlterTerm fIlter);
    }
}
