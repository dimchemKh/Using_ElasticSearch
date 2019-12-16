using System;
using System.Threading.Tasks;

namespace Using_Elasticsearch.BusinessLogic.Services.Interfaces
{
    public interface ILogExceptionService
    {
        Task Create(Exception exception, string url, Guid userId);
    }
}
