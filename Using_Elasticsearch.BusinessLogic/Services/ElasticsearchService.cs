using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Using_Elastic.DataAccess.Configs;
using Using_Elastic.DataAccess.Repositories.Interfaces;
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
    }
}
