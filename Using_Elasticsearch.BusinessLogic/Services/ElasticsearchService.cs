using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Common.Models;
using Using_Elastic.DataAccess.Entities;
using Using_Elastic.DataAccess.Repositories.Interfaces;
using Using_Elasticsearch.BusinessLogic.Common.Models;
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
            //var res = await _elasticClient.DeleteByQueryAsync<WebAppData>(x => x.Query(z => z.QueryString(c => c.Query("*"))));

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


                if (dataCount < count - 1)
                {
                    break;
                }

                GC.Collect();
            }
        }

        public async Task<IEnumerable<WebAppData>> GetRangeAsync(FilterModel filter)
        {
            var result = await _elasticClient.SearchAsync<WebAppData>(z => z
                .From(filter.From)
                .Size(filter.Size)
                .Query(x => x.Range(
                    c => c.Field(filter.ColumnName)
                    .GreaterThanOrEquals((double)filter.MinValue)
                    .LessThanOrEquals((double)filter.MaxValue)))
                );
            
            return result.Documents.ToList();
        }

        public async Task<IEnumerable<WebAppData>> GetSearchTermAsync(FIlterTerm filter)
        {
            var result = await _elasticClient.SearchAsync<WebAppData>(x => x.From(filter.From)
                                                                            .Size(filter.Size)
                                                                            .Query(z => z.Terms(t => t.Field(filter.Field).Terms(filter.Values))));

            return result.Documents.ToList();
        }
    }
}
