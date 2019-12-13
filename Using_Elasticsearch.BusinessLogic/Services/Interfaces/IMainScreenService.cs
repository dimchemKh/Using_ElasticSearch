using System.Threading.Tasks;
using Using_Elasticsearch.Common.Views.MainScreen.Request;
using Using_Elasticsearch.Common.Views.MainScreen.Response;

namespace Using_Elasticsearch.BusinessLogic.Services.Interfaces
{
    public interface IMainScreenService
    {
        Task<ResponseGetFiltersMainScreenView> GetFiltersAsync(RequestGetFiltersMainScreenView filters);
        Task<ResponseSearchMainScreenView> SearchAsync(RequestSearchMainScreenView filters);

    }
}
