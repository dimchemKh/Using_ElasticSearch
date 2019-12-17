using System;
using System.Threading.Tasks;
using Using_Elasticsearch.BusinessLogic.Services.Interfaces;
using Using_Elasticsearch.DataAccess.Entities;
using Using_Elasticsearch.DataAccess.Repositories.Interfaces;

namespace Using_Elasticsearch.BusinessLogic.Services
{
    public class LogExceptionService : ILogExceptionService
    {
        private readonly ILogExceptionRepository _repository;
        public LogExceptionService(ILogExceptionRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(Exception exception, string url, string userId)
        {
            var logException = new LogException();

            logException.StackTrace = exception.StackTrace;
            logException.Message = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
            logException.Action = url;
            logException.UserId = userId;

            await _repository.CreateAsync(logException);
        }
    }
}
