using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Using_Elasticsearch.Common.Views.AdminScreen.Request;
using Using_Elasticsearch.Common.Views.AdminScreen.Response;

namespace Using_Elasticsearch.BusinessLogic.Services.Interfaces
{
    public interface IAdminScreenService
    {
        Task<ResponseGetLogsAdminScreenView> SearchAsync(RequestGetLogsAdminScreenView requestModel);
    }
}
