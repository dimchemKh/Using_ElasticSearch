using Microsoft.Extensions.Options;
using Using_Elastic.DataAccess.Configs;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Base;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;

namespace Using_Elasticsearch.DataAccess.Repositories
{
    public class LogExceptionRepository : BaseRepository<LogException>, ILogExceptionRepository
    {
        public LogExceptionRepository(IOptions<ConnectionConfig> options) : base(options)
        {
        }
    }
}
