using System.Collections.Generic;
using System.Threading.Tasks;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Common.Views.AdminScreen.Response;

namespace Using_Elasticsearch.BusinessLogic.Services.Interfaces
{
    public interface IAdminScreenService
    {
        Task<ResponseGetPermissionsAdminScreenView> GetUserPermissionsAsync(RequestGetPermissionsAdminScreenView request);
        Task<ResponseGetUsersAdminScreenView> GetUsersAsync(RequestGetUsersAdminScreenView requestModel);
        Task<IEnumerable<string>> CreateUserAsync(RequestCreateUserAdminScreenView requestModel);
        Task UpdateUserAsync(RequestCreateUserAdminScreenView requestModel);
        Task<IEnumerable<string>> RemoveUserAsync(string userId);
    }
}
