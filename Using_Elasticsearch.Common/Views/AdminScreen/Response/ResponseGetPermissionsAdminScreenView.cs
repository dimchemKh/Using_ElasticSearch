using System.Collections.Generic;
using Using_Elasticsearch.Common.Models;

namespace Using_Elasticsearch.Common.Views.AdminScreen.Response
{
    public class ResponseGetPermissionsAdminScreenView
    {
        public IEnumerable<PermissionModel> Items { get; set; }
    }
}
