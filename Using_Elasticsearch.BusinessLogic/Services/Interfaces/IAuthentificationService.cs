using System.Threading.Tasks;
using Using_Elasticsearch.Common.Views.Authentification.Request;
using Using_Elasticsearch.Common.Views.Authentification.Response;

namespace Using_Elasticsearch.BusinessLogic.Services.Interfaces
{
    public interface IAuthentificationService
    {
        Task<ResponseGenerateAuthentificationView> LoginAsync(RequestLoginAuthentificationView requestLogin);
        Task<ResponseGenerateAuthentificationView> RefreshAsync(string email);
    }
}
