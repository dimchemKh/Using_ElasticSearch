using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Configs;
using Using_Elastic.DataAccess.Entities;
using Using_Elastic.DataAccess.Repositories.Interfaces;
using Using_Elasticsearch.Common.View.MainScreen.Post;
using Using_Elasticsearch.Common.View.Models.Get;
using Using_ElasticSearch.BusinessLogic.Services.Interfaces;

namespace Using_ElasticSearch.BusinessLogic.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IWebAppDataDapperRepository _dapperRepository;
        private readonly IElasticClient _elasticClient;
        private readonly IOptions<ConnectionConfig> _connectionConfig;

        public ElasticsearchService(IWebAppDataDapperRepository repository, IElasticClient elasticClient, IOptions<ConnectionConfig> connectionConfig)
        {
            _dapperRepository = repository;
            _elasticClient = elasticClient;
            _connectionConfig = connectionConfig;
        }

        public async Task IndexDataAsync()
        {
            var res = await _elasticClient.Indices.DeleteAsync(_connectionConfig.Value.ElasticIndex);

            var count = 10000;
            for (int i = 0; ; i += count)
            {
                var data = await _dapperRepository.GetRangeAsync(i, count);

                var dataCount = data.Count();

                if(dataCount == 0)
                {
                    break;
                }

                var response = await _elasticClient.BulkAsync(x => x
                                                   .IndexMany(data, (z, doc) => z
                                                   .Document(doc)
                                                   .Index(_connectionConfig.Value.ElasticIndex)));

                if (!response.IsValid)
                {
                    throw new Exception();
                }


                if (dataCount < count)
                {
                    break;
                }

                GC.Collect();
            }
        }

        public async Task<GetDataSearchMainView> GetRangeAsync(RequestFilterParametersMainScreen filter)
        {            
            var result = await _elasticClient.SearchAsync<WebAppData>(z => z
                .From(filter.From)
                .Size(filter.Size)
                .Query(x => x.Range(c => c
                    .Field(filter.ColumnName)
                    .GreaterThanOrEquals((double)filter.MinValue)
                    .LessThanOrEquals((double)filter.MaxValue)))
                    .SearchAfter(new List<WebAppData> {  }, ));

            var totalCount = await _elasticClient.CountAsync<WebAppData>(z => z.Query(x => x.Range(c => c
                    .Field(filter.ColumnName)
                    .GreaterThanOrEquals((double)filter.MinValue)
                    .LessThanOrEquals((double)filter.MaxValue))));

            var response = new GetDataSearchMainView();

            response.TotalCount = (int)totalCount.Count;

            response.Items = result.Documents.ToList();

            return response;
        }

        public async Task<GetDataSearchMainView> GetSearchTermAsync(RequestFilterParametersMainScreen filter)
        {
            var result = await _elasticClient.SearchAsync<WebAppData>(x => x.From(filter.From)
                                                                            .Size(filter.Size)
                                                                            .Query(z => z.Terms(t => t.Field(filter.ColumnName).Terms(filter.Values))));

            var response = new GetDataSearchMainView();

            response.Items = result.Documents.ToList();

            return response;
        }
    }
}
