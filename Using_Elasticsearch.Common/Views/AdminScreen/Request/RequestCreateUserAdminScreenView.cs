using System.Collections.Generic;
using Using_Elasticsearch.Common.Models;
using static Using_Elasticsearch.DataAccess.Entities.Enums.Enums;

namespace Using_Elasticsearch.Common.Views.AdminScreen.Request
{
    public class RequestCreateUserAdminScreenView
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public IList<PermissionModel> Permissions { get; set; }
        public RequestCreateUserAdminScreenView()
        {
            Permissions = new List<PermissionModel>();
        }
    }
}
