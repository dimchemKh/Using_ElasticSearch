using Nest;
using System.Linq;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Entities;
using Using_Elasticsearch.BusinessLogic.Helpers;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Views.MainScreen.Request;
using Using_Elasticsearch.Common.Views.MainScreen.Response;

namespace Using_Elasticsearch.BusinessLogic.Services
{
    public class MainScreenService : IMainScreenService
    {
        private readonly IElasticClient _elasticClient;
        private const string ValueKey = "totalCount";
        public MainScreenService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<ResponseGetFiltersMainScreenView> GetFiltersAsync(RequestGetFiltersMainScreenView filters)
        {
            var currentField = ElasticHelper.StringToLower(filters.CurrentFilter.ToString());

            filters.Filters.GetType().GetProperty(filters.CurrentFilter.ToString()).SetValue(filters.Filters, Enumerable.Empty<string>());

            var result = await _elasticClient.SearchAsync<WebAppData>(x => x.Query(z => z.SearchQuery(filters.Filters))
                                                                            .Aggregations(z => z.TermsInit(filters)));

            var response = new ResponseGetFiltersMainScreenView();
            
            response.Items = result.Aggregations.Terms(currentField).Buckets.Select(x => x.Key);

            return response;
        }

        public async Task<ResponseSearchMainScreenView> SearchAsync(RequestSearchMainScreenView filters)
        {
            var result = await _elasticClient.SearchAsync<WebAppData>(x => x
                                     .From(filters.From)
                                     .Size(filters.Size)
                                     .Query(z => z.SearchQuery(filters.Filters))
                                     .Aggregations(a => a.ValueCount(ValueKey, f => f.Field(r => r.RecId))));

            var response = new ResponseSearchMainScreenView();

            response.Items = result.Documents.ToList();
            response.TotalCount = (int)result.Aggregations.ValueCount(ValueKey).Value;

            return response;
        }
    }
}
