using System.Threading.Tasks;

namespace Using_ElasticSearch.BusinessLogic.Services.Interfaces
{
    public interface IElasticsearchService
    {
        Task IndexDataAsync();
        Task IndexExceptionsAsync();
    }
}
