using System.Threading.Tasks;
using Using_Elasticsearch.Common.Views.Authentification.Request;
using Using_Elasticsearch.Common.Views.Authentification.Response;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.BusinessLogic.Services.Interfaces
{
    public interface IAuthentificationService
    {
        Task<ResponseGenerateAuthentificationView> LoginAsync(RequestLoginAuthentificationView requestLogin);
        Task<ApplicationUser> FindUserAsync(string email);
    }
}
