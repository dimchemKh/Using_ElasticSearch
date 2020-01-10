using System.Threading.Tasks;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Common.Views.AdminScreen.Response;

namespace Using_Elasticsearch.BusinessLogic.Services.Interfaces
{
    public interface ILogsScreenService
    {
        Task<ResponseGetLogsAdminScreenView> GetLogsAsync(RequestGetLogsAdminScreenView requestModel);
    }
}
