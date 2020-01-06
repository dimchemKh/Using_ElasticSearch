using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Linq;
using System.Threading.Tasks;
using Using_Elasticsearch.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;
using Using_ElasticSearch.BusinessLogic.Services.Interfaces;

namespace Using_ElasticSearch.BusinessLogic.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IWebAppDataDapperRepository _webAppDataRepository;
        private readonly ILogExceptionRepository _logExceptionRepository;
        private readonly IElasticClient _elasticClient;
        private readonly IOptions<ConnectionConfig> _connectionConfig;

        public ElasticsearchService(IWebAppDataDapperRepository repository, ILogExceptionRepository logExceptionRepository, IElasticClient elasticClient, IOptions<ConnectionConfig> connectionConfig)
        {
            _webAppDataRepository = repository;
            _logExceptionRepository = logExceptionRepository;
            _elasticClient = elasticClient;
            _connectionConfig = connectionConfig;
        }

        public async Task IndexDataAsync()
        {
            var deleteResult = await _elasticClient.Indices.DeleteAsync(_connectionConfig.Value.ElasticIndex);

            var count = 10000;

            for (int i = 0; ; i += count)
            {
                var data = await _webAppDataRepository.GetRangeAsync(i, count);

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
                    throw new ProjectException(response.ApiCall.HttpStatusCode.Value);
                }


                if (dataCount < count)
                {
                    break;
                }

                GC.Collect();
            }
        }
        public async Task IndexExceptionsAsync()
        {
            var deleteResult = await _elasticClient.Indices.DeleteAsync(_connectionConfig.Value.LogIndex);

            var data = await _logExceptionRepository.GetAllAsync();

            var response = await _elasticClient.BulkAsync(x => x
                                                   .IndexMany(data, (z, doc) => z
                                                   .Document(doc)
                                                   .Index(_connectionConfig.Value.LogIndex)));

            if (!response.IsValid)
            {
                throw new ProjectException(response.ApiCall.HttpStatusCode.Value);
            }

        }
    }
}
