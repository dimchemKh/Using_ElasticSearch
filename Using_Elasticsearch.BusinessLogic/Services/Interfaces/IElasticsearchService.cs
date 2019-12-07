using System.Threading.Tasks;
using Using_Elasticsearch.Common.View.MainScreen.Post;
using Using_Elasticsearch.Common.View.Models.Get;

namespace Using_ElasticSearch.BusinessLogic.Services.Interfaces
{
    public interface IElasticsearchService
    {
        Task IndexDataAsync();
        Task<GetDataSearchMainView> GetRangeAsync(RequestFilterParametersMainScreen filter);
        Task<GetDataSearchMainView> GetSearchTermAsync(RequestFilterParametersMainScreen fIlter);
    }
}
