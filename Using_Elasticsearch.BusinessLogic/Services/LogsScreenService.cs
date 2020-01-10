﻿using Nest;
using System.Linq;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Common.Views.AdminScreen.Response;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.BusinessLogic.Services
{
    public class LogsScreenService : ILogsScreenService
    {
        private readonly IElasticClient _elasticClient;
        private const string ValueKey = "totalCount";

        public LogsScreenService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<ResponseGetLogsAdminScreenView> GetLogsAsync(RequestGetLogsAdminScreenView requestModel)
        {
            var result = await _elasticClient.SearchAsync<LogException>(x => x
                         .From(requestModel.From)
                         .Size(requestModel.Size)
                         .Query(z => z).Sort(z => z.Ascending(a => a.CreationDate))
                         .Index("log_index")
                         .Sort(s => s.Descending(a => a.CreationDate))

                         .Aggregations(a => a.ValueCount(ValueKey, f => f.Field(r => r.CreationDate))));

            var response = new ResponseGetLogsAdminScreenView();

            response.Items = result.Documents.ToList();
            response.TotalCount = (int)result.Aggregations.ValueCount(ValueKey).Value;

            return response;
        }
    }
}
