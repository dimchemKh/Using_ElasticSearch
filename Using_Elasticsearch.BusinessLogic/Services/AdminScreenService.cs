using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Common.Views.AdminScreen.Response;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.BusinessLogic.Services
{
    public class AdminScreenService : IAdminScreenService
    {
        private readonly IElasticClient _elasticClient;
        private const string ValueKey = "totalCount";
        private const string IndexName = "log_index";

        public AdminScreenService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<ResponseGetLogsAdminScreenView> SearchAsync(RequestGetLogsAdminScreenView requestModel)
        {
            var result = await _elasticClient.SearchAsync<LogException>(x => x
                         .From(requestModel.From)
                         .Size(requestModel.Size)
                         .Index(IndexName)
                         .Sort(s => s.Ascending(a => a.CreationDate))
                         .Aggregations(a => a.ValueCount(ValueKey, f => f.Field(r => r.CreationDate))));

            var response = new ResponseGetLogsAdminScreenView();

            response.Items = result.Documents.ToList();
            response.TotalCount = (int)result.Aggregations.ValueCount(ValueKey).Value;

            return response;
        }
    }
}
